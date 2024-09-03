using PetFamily.Domain.Shared.Entities.BaseDomain;
using PetFamily.Domain.Shared.Interfaces;
using PetFamily.Domain.ValueObjects.Addresses;
using PetFamily.Domain.ValueObjects.Contacts;
using PetFamily.Domain.ValueObjects.Requisites;
using PetFamily.Domain.ValueObjects.SpeciesDetails;
using PetFamily.Domain.ValueObjects.String;
using PetFamily.Domain.VolunteerManagement.Enums;
using PetFamily.Domain.VolunteerManagement.PetPhotos;

namespace PetFamily.Domain.VolunteerManagement.Pets;

public class Pet
    : Entity<PetId>, ISoftDeletable
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

    public RequisiteDetails Requisites { get; private set; }

    public SpeciesDetail SpeciesDetail { get; private set; }

    public DateTime DateCreated { get; private set; }

    private readonly List<PetPhoto> _petPhotos = [];

    public IReadOnlyList<PetPhoto> PetPhotos => _petPhotos.AsReadOnly();

    private bool _isDeleted;

    private Pet(PetId id)
        : base(id)
    {
    }

    public void Delete() => _isDeleted = true;

    public void Restore() => _isDeleted = false;
}