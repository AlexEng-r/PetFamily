/*using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects.Addresses;
using PetFamily.Domain.ValueObjects.Contacts;
using PetFamily.Domain.ValueObjects.Requisites;
using PetFamily.Domain.ValueObjects.String;
using PetFamily.Domain.VolunteerManagement.Enums;
using PetFamily.Domain.VolunteerManagement.Pets;

namespace PetFamily.Base.Test.Builder;

public static class PetBuilder
{
    public static Pet Create()
    {
        var nickName = NotEmptyString.Create("TestNick").Value;
        var animalType = NotEmptyString.Create("TestAnimalType").Value;
        var descriptionPet = new CanBeEmptyString("TestDescription");
        var color = NotEmptyString.Create("TestColor").Value;
        var healthInformation = new CanBeEmptyString("TestHealthInformation");
        var address = Address.Create("TestAddress", "TestAddress", "TestAddress").Value;
        var phonePet = ContactPhone.Create("TestPhone").Value;
        var requisites = new List<Requisite> { Requisite.Create("1", "2").Value };

        return new Pet(PetId.NewPetId(),
            nickName,
            animalType,
            descriptionPet,
            color,
            healthInformation,
            address,
            null,
            null,
            phonePet,
            false,
            null,
            false,
            StatusType.NeedHelp,
            null!,
            DateTime.Now,
            requisites);
    }
}*/