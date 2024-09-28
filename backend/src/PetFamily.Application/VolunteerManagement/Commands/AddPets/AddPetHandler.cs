using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Application.Repositories.Volunteers;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagement.Specieses;
using PetFamily.Domain.ValueObjects.Addresses;
using PetFamily.Domain.ValueObjects.Contacts;
using PetFamily.Domain.ValueObjects.Requisites;
using PetFamily.Domain.ValueObjects.SpeciesDetails;
using PetFamily.Domain.ValueObjects.String;
using PetFamily.Domain.VolunteerManagement.Pets;

namespace PetFamily.Application.VolunteerManagement.Commands.AddPets;

public class AddPetHandler
    : ICommandHandler<AddPetCommand, Guid>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddPetHandler> _logger;
    private readonly IValidator<AddPetCommand> _addPetValidator;

    public AddPetHandler(
        IVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork,
        ILogger<AddPetHandler> logger,
        IValidator<AddPetCommand> addPetValidator)
    {
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _addPetValidator = addPetValidator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(AddPetCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _addPetValidator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }
        
        var volunteerResult = await _volunteersRepository.GetById(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
        {
            return volunteerResult.Error.ToErrorList();
        }

        var pet = InitPet(command);

        volunteerResult.Value.AddPet(pet);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Added Pet #{pet.Id}", pet.Id);

        return pet.Id.Value;
    }

    private Pet InitPet(AddPetCommand command)
    {
        var petId = PetId.NewPetId();
        var nickName = NotEmptyString.Create(command.NickName).Value;
        var animalType = NotEmptyString.Create(command.AnimalType).Value;
        var description = new CanBeEmptyString(command.Description);
        var breed = new CanBeEmptyString(command.Breed);
        var color = NotEmptyString.Create(command.Color).Value;
        var healthInformation = new CanBeEmptyString(command.HealthInformation);
        var address = Address.Create(command.AddressDto.City, command.AddressDto.House, command.AddressDto.Flat).Value;
        var phone = ContactPhone.Create(command.Phone).Value;
        var speciesDetail = new SpeciesDetail(SpeciesId.Empty(), Guid.Empty);
        var dateCreated = DateTime.Now.ToUniversalTime();
        var requisites = command.Requisites.Select(x => Requisite.Create(x.Name, x.Description).Value).ToList();

        var pet = new Pet(petId, nickName, animalType, description, breed, color, healthInformation, address,
            command.Weight, command.Height, phone, command.IsSterialized, command.Birthday,
            command.IsVaccinated, command.Status, speciesDetail, dateCreated, requisites);

        return pet;
    }
}