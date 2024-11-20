using CSharpFunctionalExtensions;
using PetFamily.Core.Enums;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.Interfaces;
using PetFamily.SharedKernel.ValueObjects.Contacts;
using PetFamily.SharedKernel.ValueObjects.FullNames;
using PetFamily.SharedKernel.ValueObjects.Ids;
using PetFamily.SharedKernel.ValueObjects.Positions;
using PetFamily.SharedKernel.ValueObjects.Requisites;
using PetFamily.SharedKernel.ValueObjects.SocialNetworks;
using PetFamily.SharedKernel.ValueObjects.String;
using PetFamily.VolunteerManagement.Domain.Entities.Pets;

namespace PetFamily.VolunteerManagement.Domain;

public class Volunteer
    : SharedKernel.Entities.BaseDomain.Entity<VolunteerId>, ISoftDeletable
{
    public FullName FullName { get; private set; }

    public NotEmptyString Description { get; private set; }

    public int Experience { get; private set; }

    public ContactPhone Phone { get; private set; }

    private List<SocialNetwork> _socialNetworks = [];

    public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks.AsReadOnly();

    private List<Requisite> _requisites = [];

    public IReadOnlyList<Requisite> Requisites => _requisites.AsReadOnly();

    private readonly List<Pet> _pets = [];

    public IReadOnlyList<Pet> Pets => _pets.AsReadOnly();

    public int PetsAdoptedCount => Pets.Count(x => x.Status == StatusType.FoundAHome);

    public int PetsInSearchCount => Pets.Count(x => x.Status == StatusType.LookingForAHome);

    public int PetsOnTreatment => Pets.Count(x => x.Status == StatusType.NeedHelp);

    public bool IsDeleted {get; private set;}

    private Volunteer(VolunteerId id)
        : base(id)
    {
    }

    public Volunteer(VolunteerId id,
        FullName fullName,
        NotEmptyString description,
        int experience,
        ContactPhone phone)
        : base(id)
    {
        UpdateMainInfo(fullName, description, experience, phone);
    }

    public Volunteer UpdateMainInfo(FullName fullName,
        NotEmptyString description,
        int experience,
        ContactPhone phone)
    {
        FullName = fullName;
        Description = description;
        Experience = experience;
        Phone = phone;

        return this;
    }

    public Volunteer SetSocialNetworks(IEnumerable<SocialNetwork> socialNetworks)
    {
        _socialNetworks = socialNetworks.ToList();

        return this;
    }

    public Volunteer SetRequisites(IEnumerable<Requisite> requisites)
    {
        _requisites = requisites.ToList();

        return this;
    }

    public Pet? GetPetById(PetId petId)
        => _pets.FirstOrDefault(x => x.Id == petId);

    public UnitResult<Error> AddPet(Pet pet)
    {
        var serialNumberResult = Position.Create(_pets.Count + 1);
        if (serialNumberResult.IsFailure)
        {
            return serialNumberResult.Error;
        }

        pet.SetPosition(serialNumberResult.Value);

        _pets.Add(pet);

        return Result.Success<Error>();
    }

    public UnitResult<Error> MovePet(Pet pet, Position newPosition)
    {
        var currentPosition = pet.Position;
        if (currentPosition == newPosition || _pets.Count == 1)
        {
            return Result.Success<Error>();
        }

        var adjustedPosition = AdjustNewPositionIfOutOfRange(newPosition);
        if (adjustedPosition.IsFailure)
        {
            return adjustedPosition.Error;
        }

        newPosition = adjustedPosition.Value;

        var movePosition = MovePetBetweenPositions(newPosition, currentPosition);
        if (movePosition.IsFailure)
        {
            return movePosition.Error;
        }

        pet.SetPosition(newPosition);

        return Result.Success<Error>();
    }

    private Result<Position, Error> AdjustNewPositionIfOutOfRange(Position newPosition)
    {
        if (newPosition.Value <= _pets.Count)
        {
            return newPosition;
        }

        var lastPosition = Position.Create(_pets.Count - 1);
        if (lastPosition.IsFailure)
        {
            return lastPosition.Error;
        }

        return lastPosition.Value;
    }

    private UnitResult<Error> MovePetBetweenPositions(Position newPosition, Position currentPosition)
    {
        if (newPosition.Value < currentPosition.Value)
        {
            var petsToMove = _pets.Where(x =>
                x.Position.Value >= newPosition.Value &&
                x.Position.Value < currentPosition.Value);

            foreach (var petToMove in petsToMove)
            {
                var result = petToMove.MoveToForward();
                if (result.IsFailure)
                {
                    return result.Error;
                }
            }
        }
        else if (newPosition.Value > currentPosition.Value)
        {
            var petsToMove = _pets.Where(x =>
                x.Position.Value <= newPosition.Value &&
                x.Position.Value > currentPosition.Value);

            foreach (var petToMove in petsToMove)
            {
                var result = petToMove.MoveBack();
                if (result.IsFailure)
                {
                    return result.Error;
                }
            }
        }

        return Result.Success<Error>();
    }

    public void Delete() => IsDeleted = true;

    public void Restore() => IsDeleted = false;
}