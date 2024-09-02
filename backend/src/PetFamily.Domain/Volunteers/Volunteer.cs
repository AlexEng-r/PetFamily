using PetFamily.Domain.Contacts;
using PetFamily.Domain.FullNames;
using PetFamily.Domain.Pets;
using PetFamily.Domain.Requisites;
using PetFamily.Domain.SeedWork;
using PetFamily.Domain.SeedWork.Entities.BaseDomain;
using PetFamily.Domain.SocialNetworks;
using PetFamily.Domain.String;

namespace PetFamily.Domain.Volunteers;

public class Volunteer
    : Entity<VolunteerId>, ISoftDeletable
{
    public FullName FullName { get; private set; }

    public NotEmptyString Description { get; private set; }

    public int Experience { get; private set; }

    public ContactPhone Phone { get; private set; }

    public SocialNetworkDetails? SocialNetworks { get; private set; }

    public RequisiteDetails? Requisites { get; private set; }

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

    public Volunteer SetSocialNetworks(IEnumerable<SocialNetwork> socialNetworks)
    {
        SocialNetworks = new SocialNetworkDetails(socialNetworks);

        return this;
    }

    public Volunteer SetRequisites(IEnumerable<Requisite> requisites)
    {
        Requisites = new RequisiteDetails(requisites);

        return this;
    }

    public void Delete() => _isDeleted = true;

    public void Restore() => _isDeleted = false;
}