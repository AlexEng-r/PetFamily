﻿using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Interfaces;
using PetFamily.Domain.ValueObjects.Contacts;
using PetFamily.Domain.ValueObjects.FullNames;
using PetFamily.Domain.ValueObjects.Positions;
using PetFamily.Domain.ValueObjects.Requisites;
using PetFamily.Domain.ValueObjects.SocialNetworks;
using PetFamily.Domain.ValueObjects.String;
using PetFamily.Domain.VolunteerManagement.Enums;
using PetFamily.Domain.VolunteerManagement.Pets;

namespace PetFamily.Domain.VolunteerManagement.Volunteers;

public class Volunteer
    : Shared.Entities.BaseDomain.Entity<VolunteerId>, ISoftDeletable
{
    public FullName FullName { get; private set; }

    public NotEmptyString Description { get; private set; }

    public int Experience { get; private set; }

    public ContactPhone Phone { get; private set; }

    public ValueObjectList<SocialNetwork>? SocialNetworks { get; private set; }

    public ValueObjectList<Requisite>? Requisites { get; private set; }

    private readonly List<Pet> _pets = [];

    public IReadOnlyList<Pet> Pets => _pets.AsReadOnly();

    public int PetsAdoptedCount => Pets.Count(x => x.Status == StatusType.FoundAHome);

    public int PetsInSearchCount => Pets.Count(x => x.Status == StatusType.LookingForAHome);

    public int PetsOnTreatment => Pets.Count(x => x.Status == StatusType.NeedHelp);

    private bool _isDeleted;

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

    public Volunteer SetSocialNetworks(ValueObjectList<SocialNetwork> socialNetworks)
    {
        SocialNetworks = socialNetworks;

        return this;
    }

    public Volunteer SetRequisites(ValueObjectList<Requisite> requisites)
    {
        Requisites = requisites;

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

    public void Delete() => _isDeleted = true;

    public void Restore() => _isDeleted = false;
}