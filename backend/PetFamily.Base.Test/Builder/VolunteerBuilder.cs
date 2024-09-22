using PetFamily.Domain.ValueObjects.Contacts;
using PetFamily.Domain.ValueObjects.FullNames;
using PetFamily.Domain.ValueObjects.String;
using PetFamily.Domain.VolunteerManagement.Volunteers;

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