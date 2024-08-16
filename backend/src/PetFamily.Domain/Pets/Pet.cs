using PetFamily.Domain.Addresses;
using PetFamily.Domain.Contacts;
using PetFamily.Domain.PetPhotos;
using PetFamily.Domain.Requisites;
using PetFamily.Domain.SeedWork.Entities.BaseDomain;
using PetFamily.Domain.SpeciesDetails;
using PetFamily.Domain.String;

namespace PetFamily.Domain.Pets;

public class Pet
    : Entity<PetId>
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

    private Pet(PetId id)
        : base(id)
    {
    }
}