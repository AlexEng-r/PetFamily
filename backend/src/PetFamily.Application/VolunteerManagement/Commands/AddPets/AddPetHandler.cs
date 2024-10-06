using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Application.Repositories.Specieses;
using PetFamily.Application.Repositories.Volunteers;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagement.Specieses;
using PetFamily.Domain.ValueObjects.Addresses;
using PetFamily.Domain.ValueObjects.Contacts;
using PetFamily.Domain.ValueObjects.Requisites;
using PetFamily.Domain.ValueObjects.String;
using PetFamily.Domain.VolunteerManagement.Pets;
using PetFamily.Domain.VolunteerManagement.Pets.SpeciesDetails;

namespace PetFamily.Application.VolunteerManagement.Commands.AddPets;

public class AddPetHandler
    : ICommandHandler<AddPetCommand, Guid>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddPetHandler> _logger;
    private readonly IValidator<AddPetCommand> _addPetValidator;
    private readonly ISpeciesRepository _speciesRepository;

    public AddPetHandler(
        IVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork,
        ILogger<AddPetHandler> logger,
        IValidator<AddPetCommand> addPetValidator,
        ISpeciesRepository speciesRepository)
    {
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _addPetValidator = addPetValidator;
        _speciesRepository = speciesRepository;
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

        var isSpeciesAndBreedNotNull = command.SpeciesId.HasValue || command.BreedId.HasValue;
        if (isSpeciesAndBreedNotNull)
        {
            var species = await _speciesRepository.GetById(command.SpeciesId!, cancellationToken);
            if (species.IsFailure)
            {
                return species.Error.ToErrorList();
            }

            var breed = species.Value.Breeds.FirstOrDefault(x => x.Id.Value == command.BreedId);
            if (breed == null)
            {
                return Errors.General.NotFound(command.BreedId).ToErrorList();
            }
        }

        var pet = InitPet(command, isSpeciesAndBreedNotNull);

        volunteerResult.Value.AddPet(pet);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Added Pet #{pet.Id}", pet.Id);

        return pet.Id.Value;
    }

    private Pet InitPet(AddPetCommand command, bool isSpeciesAndBreedNotNull)
    {
        var petId = PetId.NewPetId();
        var nickName = NotEmptyString.Create(command.NickName).Value;
        var animalType = NotEmptyString.Create(command.AnimalType).Value;
        var description = new CanBeEmptyString(command.Description);
        var color = NotEmptyString.Create(command.Color).Value;
        var healthInformation = new CanBeEmptyString(command.HealthInformation);
        var address = Address.Create(command.AddressDto.City, command.AddressDto.House, command.AddressDto.Flat).Value;
        var phone = ContactPhone.Create(command.Phone).Value;
        var dateCreated = DateTime.Now.ToUniversalTime();
        var requisites = command.Requisites.Select(x => Requisite.Create(x.Name, x.Description).Value).ToList();

        var speciesDetail = isSpeciesAndBreedNotNull
            ? new SpeciesDetail(SpeciesId.Create(command.SpeciesId!.Value), command.BreedId!.Value)
            : new SpeciesDetail(SpeciesId.Empty(), Guid.Empty);

        var pet = new Pet(petId, nickName, animalType, description, color, healthInformation, address,
            command.Weight, command.Height, phone, command.IsSterialized, command.Birthday,
            command.IsVaccinated, command.Status, speciesDetail, dateCreated, requisites);

        return pet;
    }
}