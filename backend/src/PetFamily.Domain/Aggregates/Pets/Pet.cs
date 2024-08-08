using PetFamily.Domain.Aggregates.Requisites;
using PetFamily.Domain.SeedWork.Entities;
using PetFamily.Domain.SeedWork.Entities.BaseDomain;

namespace PetFamily.Domain.Aggregates.Pets;

public class Pet
    : DomainEntity, IAggregateRoot
{
    public string NickName { get; }

    public AnimalType AnimalType { get; }

    public string Color { get; private set; }

    public string CurrentPetAddress { get; }

    public string Phone { get; }

    public bool IsSterialized { get; }

    public bool IsVaccinated { get; }

    public StatusType Status { get; }

    public PetOption? PetOptions { get; private set; }

    public Requisite? Requisite { get; private set; }

    private Pet()
    {
    }

    public Pet(string nickName, AnimalType animalType, string color, string currentPetAddress, string phone,
        bool isSterialized, bool isVaccinated, StatusType status)
    {
        NickName = nickName;
        AnimalType = animalType;
        Color = color;
        CurrentPetAddress = currentPetAddress;
        Phone = phone;
        IsSterialized = isSterialized;
        IsVaccinated = isVaccinated;
        Status = status;
    }

    public Pet SetPetOption(PetOption petOption)
    {
        PetOptions = petOption;

        return this;
    }

    public Pet SetRequisite(Requisite requisite)
    {
        Requisite = requisite;

        return this;
    }
}