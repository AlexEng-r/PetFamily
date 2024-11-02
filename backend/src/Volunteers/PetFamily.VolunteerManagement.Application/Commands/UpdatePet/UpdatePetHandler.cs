using CSharpFunctionalExtensions;
using FluentValidation;
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

namespace PetFamily.VolunteerManagement.Application.Commands.UpdatePet;

public class UpdatePetHandler
    : ICommandHandler<UpdatePetCommand, Guid>
{
    private readonly IValidator<UpdatePetCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ISpeciesContract _speciesContract;
    private readonly IVolunteerUnitOfWork _volunteerUnitOfWork;

    public UpdatePetHandler(IValidator<UpdatePetCommand> validator,
        IVolunteersRepository volunteersRepository,
        ISpeciesContract speciesContract,
        IVolunteerUnitOfWork volunteerUnitOfWork)
    {
        _validator = validator;
        _volunteersRepository = volunteersRepository;
        _speciesContract = speciesContract;
        _volunteerUnitOfWork = volunteerUnitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> Handle(UpdatePetCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }

        var volunteer = await _volunteersRepository.GetById(command.VolunteerId, cancellationToken);
        if (volunteer.IsFailure)
        {
            return volunteer.Error.ToErrorList();
        }

        var petId = PetId.Create(command.PetId);

        var pet = volunteer.Value.Pets.FirstOrDefault(x => x.Id == petId);
        if (pet == null)
        {
            return Errors.General.NotFound(petId).ToErrorList();
        }

        var isSpeciesAndBreedNotNull = command.SpeciesId.HasValue && command.BreedId.HasValue;
        if (isSpeciesAndBreedNotNull)
        {
            var species = await _speciesContract.GetSpeciesById(command.SpeciesId!.Value, cancellationToken);
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

        var result = UpdatePet(pet, command, isSpeciesAndBreedNotNull);

        await _volunteerUnitOfWork.SaveChanges(cancellationToken);

        return result;
    }

    private Guid UpdatePet(Pet pet, UpdatePetCommand command, bool isSpeciesAndBreedNotNull)
    {
        var nickName = NotEmptyString.Create(command.NickName).Value;
        var animalType = NotEmptyString.Create(command.AnimalType).Value;
        var description = new CanBeEmptyString(command.Description);
        var color = NotEmptyString.Create(command.Color).Value;
        var healthInformation = new CanBeEmptyString(command.HealthInformation);
        var address = Address.Create(command.AddressDto.City, command.AddressDto.House, command.AddressDto.Flat).Value;
        var phone = ContactPhone.Create(command.Phone).Value;
        var requisites = command.Requisites.Select(x => Requisite.Create(x.Name, x.Description).Value).ToList();

        var speciesDetail = isSpeciesAndBreedNotNull
            ? new SpeciesDetail(SpeciesId.Create(command.SpeciesId!.Value), command.BreedId!.Value)
            : new SpeciesDetail(SpeciesId.Empty(), Guid.Empty);

        pet.UpdateMainInfo(nickName,
            animalType,
            description,
            color,
            healthInformation,
            address,
            command.Weight,
            command.Height,
            phone,
            command.IsSterialized,
            command.Birthday,
            command.IsVaccinated,
            command.Status,
            speciesDetail,
            requisites);

        return pet.Id;
    }
}