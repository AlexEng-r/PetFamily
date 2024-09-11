using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Entities.BaseDomain;
using PetFamily.Domain.Shared.Interfaces;
using PetFamily.Domain.ValueObjects.Contacts;
using PetFamily.Domain.ValueObjects.FullNames;
using PetFamily.Domain.ValueObjects.Requisites;
using PetFamily.Domain.ValueObjects.SocialNetworks;
using PetFamily.Domain.ValueObjects.String;
using PetFamily.Domain.VolunteerManagement.Enums;
using PetFamily.Domain.VolunteerManagement.Pets;

namespace PetFamily.Domain.VolunteerManagement.Volunteers;

public class Volunteer
    : Entity<VolunteerId>, ISoftDeletable
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

    public Volunteer AddPet(Pet pet)
    {
        // доп логика

        _pets.Add(pet);

        return this;
    }

    public void Delete() => _isDeleted = true;

    public void Restore() => _isDeleted = false;
}