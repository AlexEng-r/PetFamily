using PetFamily.SharedKernel.ValueObjects.Contacts;
using PetFamily.SharedKernel.ValueObjects.FullNames;
using PetFamily.SharedKernel.ValueObjects.Ids;
using PetFamily.SharedKernel.ValueObjects.String;
using PetFamily.VolunteerManagement.Domain;

namespace PetFamily.Base.Test.Builder;

public static class VolunteerBuilder
{
    public static Volunteer GetVolunteerWithPets(int petCount)
    {
        var fullName = FullName.Create("Test", "Test", "Test").Value;
        var description = NotEmptyString.Create("TestDescription").Value;
        var phone = ContactPhone.Create("TestPhone").Value;

        var volunteer = new Volunteer(VolunteerId.NewVolunteerId(), fullName, description, 0, phone);

        for (int i = 0; i < petCount; i++)
        {
            var pet = PetBuilder.Create();
            volunteer.AddPet(pet);
        }

        return volunteer;
    }
}