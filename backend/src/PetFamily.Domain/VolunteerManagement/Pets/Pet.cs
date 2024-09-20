using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Interfaces;
using PetFamily.Domain.ValueObjects.Addresses;
using PetFamily.Domain.ValueObjects.Contacts;
using PetFamily.Domain.ValueObjects.Positions;
using PetFamily.Domain.ValueObjects.Requisites;
using PetFamily.Domain.ValueObjects.SpeciesDetails;
using PetFamily.Domain.ValueObjects.String;
using PetFamily.Domain.VolunteerManagement.Enums;
using PetFamily.Domain.VolunteerManagement.PetPhotos;

namespace PetFamily.Domain.VolunteerManagement.Pets;

public class Pet
    : Shared.Entities.BaseDomain.Entity<PetId>, ISoftDeletable
{
    public NotEmptyString NickName { get; private set; }

    public NotEmptyString AnimalType { get; private set; }

    public CanBeEmptyString Description { get; private set; }

    public CanBeEmptyString Breed { get; private set; }

    public NotEmptyString Color { get; private set; }

    public CanBeEmptyString HealthInformation { get; private set; }

    public Address Address { get; private set; }

    public double? Weight { get; private set; }

    public double? Height { get; private set; }

    public ContactPhone Phone { get; private set; }

    public bool IsSterialized { get; private set; }

    public DateTime? BirthDayDate { get; private set; }

    public bool IsVaccinated { get; private set; }

    public StatusType Status { get; private set; }

    public ValueObjectList<Requisite> Requisites { get; private set; }

    public SpeciesDetail SpeciesDetail { get; private set; }

    public Position Position { get; private set; }

    public DateTime DateCreated { get; private set; }

    private readonly List<PetPhoto> _petPhotos = [];

    public IReadOnlyList<PetPhoto> PetPhotos => _petPhotos.AsReadOnly();

    private bool _isDeleted;

    private Pet(PetId id)
        : base(id)
    {
    }

    public Pet(PetId petId,
        NotEmptyString nickName,
        NotEmptyString animalType,
        CanBeEmptyString description,
        CanBeEmptyString breed,
        NotEmptyString color,
        CanBeEmptyString healthInformation,
        Address address,
        double? weight,
        double? height,
        ContactPhone phone,
        bool isSterialized,
        DateTime? birthDayDate,
        bool isVaccinated,
        StatusType status,
        SpeciesDetail speciesDetail,
        DateTime dateCreated,
        ValueObjectList<Requisite> requisites)
        : base(petId)
    {
        DateCreated = dateCreated;

        UpdateMainInfo(nickName,
            animalType,
            description,
            breed, color,
            healthInformation,
            address,
            weight,
            height,
            phone,
            isSterialized,
            birthDayDate,
            isVaccinated,
            status,
            speciesDetail,
            requisites);
    }

    public Pet UpdateMainInfo(NotEmptyString nickName,
        NotEmptyString animalType,
        CanBeEmptyString description,
        CanBeEmptyString breed,
        NotEmptyString color,
        CanBeEmptyString healthInformation,
        Address address,
        double? weight,
        double? height,
        ContactPhone phone,
        bool isSterialized,
        DateTime? birthDayDate,
        bool isVaccinated,
        StatusType status,
        SpeciesDetail speciesDetail,
        ValueObjectList<Requisite> requisites)
    {
        NickName = nickName;
        AnimalType = animalType;
        Description = description;
        Breed = breed;
        Color = color;
        HealthInformation = healthInformation;
        Address = address;
        Weight = weight;
        Height = height;
        Phone = phone;
        IsSterialized = isSterialized;
        BirthDayDate = birthDayDate;
        IsVaccinated = isVaccinated;
        Status = status;
        SpeciesDetail = speciesDetail;
        Requisites = requisites;

        return this;
    }

    public Pet AddPetPhotos(IEnumerable<PetPhoto> photos)
    {
        _petPhotos.AddRange(photos);

        return this;
    }

    public Pet SetPosition(Position position)
    {
        Position = position;

        return this;
    }

    public void Delete() => _isDeleted = true;

    public void Restore() => _isDeleted = false;

    public UnitResult<Error> MoveToForward()
    {
        var newPosition = Position.Forward();
        if (newPosition.IsFailure)
        {
            return newPosition.Error;
        }

        Position = newPosition.Value;

        return Result.Success<Error>();
    }

    public UnitResult<Error> MoveBack()
    {
        var newPosition = Position.Back();
        if (newPosition.IsFailure)
        {
            return newPosition.Error;
        }

        Position = newPosition.Value;

        return Result.Success<Error>();
    }
}