using CSharpFunctionalExtensions;
using PetFamily.Core.Enums;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.Interfaces;
using PetFamily.SharedKernel.ValueObjects.Addresses;
using PetFamily.SharedKernel.ValueObjects.Contacts;
using PetFamily.SharedKernel.ValueObjects.Ids;
using PetFamily.SharedKernel.ValueObjects.Positions;
using PetFamily.SharedKernel.ValueObjects.Requisites;
using PetFamily.SharedKernel.ValueObjects.String;
using PetFamily.VolunteerManagement.Domain.Entities.PetPhotos;
using PetFamily.VolunteerManagement.Domain.Entities.Pets.SpeciesDetails;

namespace PetFamily.VolunteerManagement.Domain.Entities.Pets;

public class Pet
    : SharedKernel.Entities.BaseDomain.Entity<PetId>, ISoftDeletable
{
    public NotEmptyString NickName { get; private set; }

    public NotEmptyString AnimalType { get; private set; }

    public CanBeEmptyString Description { get; private set; }

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

    private List<Requisite> _requisites = [];

    public IReadOnlyList<Requisite> Requisites => _requisites.AsReadOnly();

    public SpeciesDetail SpeciesDetail { get; private set; }

    public Position Position { get; private set; }

    public DateTime DateCreated { get; private set; }

    private readonly List<PetPhoto> _petPhotos = [];

    public IReadOnlyList<PetPhoto> PetPhotos => _petPhotos.AsReadOnly();

    public bool IsDeleted { get; private set; }

    private Pet(PetId id)
        : base(id)
    {
    }

    public Pet(PetId petId,
        NotEmptyString nickName,
        NotEmptyString animalType,
        CanBeEmptyString description,
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
            color,
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
        IEnumerable<Requisite> requisites)
    {
        NickName = nickName;
        AnimalType = animalType;
        Description = description;
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
        _requisites = requisites.ToList();

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

    public Pet SetStatus(StatusType status)
    {
        Status = status;

        return this;
    }

    public void Delete() => IsDeleted = true;

    public void Restore() => IsDeleted = false;

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