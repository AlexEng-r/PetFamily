using PetFamily.Domain.Fullname;
using PetFamily.Domain.Pets;
using PetFamily.Domain.Requisites;
using PetFamily.Domain.SocialNetworks;

namespace PetFamily.Domain.Volunteers;

public class Volunteer
{
    public Name FullName { get; private set; }

    public string Description { get; private set; }

    public int Experience { get; private set; }

    public int PetsAdoptedCount { get; private set; }

    public int PetsInSearchCount { get; private set; }

    public int PetsOnTreatment { get; private set; }

    public string Phone { get; private set; }

    public List<SocialNetwork> SocialNetworks { get; private set; }

    public List<Requisite> Requisites { get; private set; }

    public List<Pet> Pets { get; private set; }
}