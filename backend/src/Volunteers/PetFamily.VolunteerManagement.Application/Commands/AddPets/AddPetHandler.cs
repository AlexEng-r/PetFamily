using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects.Addresses;
using PetFamily.SharedKernel.ValueObjects.Contacts;
using PetFamily.SharedKernel.ValueObjects.Ids;
using PetFamily.SharedKernel.ValueObjects.Requisites;
using PetFamily.SharedKernel.ValueObjects.String;
using PetFamily.SpeciesManagement.Contracts;
using PetFamily.VolunteerManagement.Application.Repositories;
using PetFamily.VolunteerManagement.Domain.Entities.Pets;
using PetFamily.VolunteerManagement.Domain.Entities.Pets.SpeciesDetails;

namespace PetFamily.VolunteerManagement.Application.Commands.AddPets;

public class AddPetHandler
    : ICommandHandler<AddPetCommand, Guid>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IVolunteerUnitOfWork _volunteerUnitOfWork;
    private readonly ILogger<AddPetHandler> _logger;
    private readonly IValidator<AddPetCommand> _addPetValidator;
    private readonly ISpeciesContract _speciesContract;

    public AddPetHandler(
        IVolunteersRepository volunteersRepository,
        IVolunteerUnitOfWork volunteerUnitOfWork,
        ILogger<AddPetHandler> logger,
        IValidator<AddPetCommand> addPetValidator,
        ISpeciesContract speciesContract)
    {
        _volunteersRepository = volunteersRepository;
        _volunteerUnitOfWork = volunteerUnitOfWork;
        _logger = logger;
        _addPetValidator = addPetValidator;
        _speciesContract = speciesContract;
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

        var isSpeciesAndBreedNotNull = command.SpeciesId.HasValue && command.BreedId.HasValue;
        if (isSpeciesAndBreedNotNull)
        {
            var species = await _speciesContract.GetSpeciesById(command.SpeciesId!.Value, cancellationToken);
            if (species.IsFailure)
            {
                return species.Error.ToErrorList();
            }

            var breed = species.Value.Breeds.FirstOrDefault(x => x.Id == command.BreedId);
            if (breed == null)
            {
                return Errors.General.NotFound(command.BreedId).ToErrorList();
            }
        }

        var pet = InitPet(command, isSpeciesAndBreedNotNull);

        volunteerResult.Value.AddPet(pet);

        await _volunteerUnitOfWork.SaveChanges(cancellationToken);

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