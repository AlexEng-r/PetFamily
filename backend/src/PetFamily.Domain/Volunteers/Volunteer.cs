using PetFamily.Domain.Fullname;
using PetFamily.Domain.Pets;
using PetFamily.Domain.Requisites;
using PetFamily.Domain.SeedWork.Entities.BaseDomain;
using PetFamily.Domain.SocialNetworks;

namespace PetFamily.Domain.Volunteers;

public class Volunteer
    : Entity
{
    public FullName FullName { get; private set; }

    public string Description { get; private set; }

    public int Experience { get; private set; }

    public int PetsAdoptedCount => Pets.Count(x => x.Status == StatusType.FoundAHome);

    public int PetsInSearchCount => Pets.Count(x => x.Status == StatusType.LookingForAHome);

    public int PetsOnTreatment => Pets.Count(x => x.Status == StatusType.NeedHelp);

    public string Phone { get; private set; }

    public SocialNetworkDetails SocialNetworks { get; private set; }

    public RequisiteDetails Requisites { get; private set; }

    private List<Pet> _pets = [];

    public IReadOnlyList<Pet> Pets => _pets.AsReadOnly();
}