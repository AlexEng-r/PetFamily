using FluentAssertions;
using PetFamily.Domain.ValueObjects.Addresses;
using PetFamily.Domain.ValueObjects.Contacts;
using PetFamily.Domain.ValueObjects.FullNames;
using PetFamily.Domain.ValueObjects.Positions;
using PetFamily.Domain.ValueObjects.String;
using PetFamily.Domain.VolunteerManagement.Enums;
using PetFamily.Domain.VolunteerManagement.Pets;
using PetFamily.Domain.VolunteerManagement.Volunteers;

namespace PetFamily.UnitTest;

public class VolunteerTest
{
    [Fact]
    public void Add_Pet_With_Empty_Pet_Return_Success()
    {
        //arrange
        var volunteer = CreateVolunteerWithPet(0);
        var pet = CreatePet();

        //act
        var result = volunteer.AddPet(pet);

        //assert
        var addedPet = volunteer.GetPetById(pet.Id);

        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        addedPet!.Id.Should().Be(pet.Id);
        addedPet.Position.Should().Be(Position.First());
    }

    [Fact]
    public void Add_Pet_With_Other_Pet_Return_Success()
    {
        //arrage
        const int PET_COUNT = 5;

        var volunteer = CreateVolunteerWithPet(PET_COUNT);
        var petToAdd = CreatePet();

        //act
        var result = volunteer.AddPet(petToAdd);
        var serialNumber = Position.Create(PET_COUNT + 1).Value;

        //assert
        var addedPet = volunteer.GetPetById(petToAdd.Id);

        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        addedPet!.Id.Should().Be(petToAdd.Id);
        addedPet.Position.Should().Be(serialNumber);
    }

    [Fact]
    public void Move_Pet_Should_Not_Move_When_Pet_Already_NewPosition()
    {
        // arrange
        const int PET_COUNT = 5;

        var volunteer = CreateVolunteerWithPet(PET_COUNT);
        var secondPosition = Position.Create(2).Value;

        var firstPet = volunteer.Pets[0];
        var secondPet = volunteer.Pets[1];
        var thirdPet = volunteer.Pets[2];
        var fourthPet = volunteer.Pets[3];
        var fifthPet = volunteer.Pets[4];

        // act
        var result = volunteer.MovePet(secondPet, secondPosition);

        // assert
        result.IsSuccess.Should().BeTrue();
        firstPet.Position.Should().Be(Position.Create(1).Value);
        secondPet.Position.Should().Be(Position.Create(2).Value);
        thirdPet.Position.Should().Be(Position.Create(3).Value);
        fourthPet.Position.Should().Be(Position.Create(4).Value);
        fifthPet.Position.Should().Be(Position.Create(5).Value);
    }

    [Fact]
    public void Move_Pet_Should_Move_Other_Pets_Forward_When_New_Position_Is_Lower()
    {
        // arrange
        const int PET_COUNT = 5;

        var volunteer = CreateVolunteerWithPet(PET_COUNT);
        var secondPosition = Position.Create(2).Value;

        var firstPet = volunteer.Pets[0];
        var secondPet = volunteer.Pets[1];
        var thirdPet = volunteer.Pets[2];
        var fourthPet = volunteer.Pets[3];
        var fifthPet = volunteer.Pets[4];

        // act
        var result = volunteer.MovePet(fourthPet, secondPosition);

        // assert
        result.IsSuccess.Should().BeTrue();
        firstPet.Position.Should().Be(Position.Create(1).Value);
        secondPet.Position.Should().Be(Position.Create(3).Value);
        thirdPet.Position.Should().Be(Position.Create(4).Value);
        fourthPet.Position.Should().Be(Position.Create(2).Value);
        fifthPet.Position.Should().Be(Position.Create(5).Value);
    }

    [Fact]
    public void Move_Pet_Should_Move_Other_Pets_Back_When_New_Position_Is_Grater()
    {
        // arrange
        const int PET_COUNT = 5;

        var volunteer = CreateVolunteerWithPet(PET_COUNT);
        var fourthPosition = Position.Create(4).Value;

        var firstPet = volunteer.Pets[0];
        var secondPet = volunteer.Pets[1];
        var thirdPet = volunteer.Pets[2];
        var fourthPet = volunteer.Pets[3];
        var fifthPet = volunteer.Pets[4];

        // act
        var result = volunteer.MovePet(secondPet, fourthPosition);

        // assert
        result.IsSuccess.Should().BeTrue();
        firstPet.Position.Should().Be(Position.Create(1).Value);
        secondPet.Position.Should().Be(Position.Create(4).Value);
        thirdPet.Position.Should().Be(Position.Create(2).Value);
        fourthPet.Position.Should().Be(Position.Create(3).Value);
        fifthPet.Position.Should().Be(Position.Create(5).Value);
    }

    [Fact]
    public void Move_Pet_Should_Move_Other_Pets_Forward_When_New_Position_Is_First()
    {
        // arrange
        const int PET_COUNT = 5;

        var volunteer = CreateVolunteerWithPet(PET_COUNT);
        var firstPosition = Position.Create(1).Value;

        var firstPet = volunteer.Pets[0];
        var secondPet = volunteer.Pets[1];
        var thirdPet = volunteer.Pets[2];
        var fourthPet = volunteer.Pets[3];
        var fifthPet = volunteer.Pets[4];

        // act
        var result = volunteer.MovePet(secondPet, firstPosition);

        // assert
        result.IsSuccess.Should().BeTrue();
        firstPet.Position.Should().Be(Position.Create(2).Value);
        secondPet.Position.Should().Be(Position.Create(1).Value);
        thirdPet.Position.Should().Be(Position.Create(3).Value);
        fourthPet.Position.Should().Be(Position.Create(4).Value);
        fifthPet.Position.Should().Be(Position.Create(5).Value);
    }

    [Fact]
    public void Move_Issue_Should_Move_Other_Issues_Back_When_New_Position_Is_Last()
    {
        // arrange
        const int PET_COUNT = 5;

        var volunteer = CreateVolunteerWithPet(PET_COUNT);
        var fifthPosition = Position.Create(5).Value;

        var firstPet = volunteer.Pets[0];
        var secondPet = volunteer.Pets[1];
        var thirdPet = volunteer.Pets[2];
        var fourthPet = volunteer.Pets[3];
        var fifthPet = volunteer.Pets[4];

        // act
        var result = volunteer.MovePet(firstPet, fifthPosition);

        // assert
        result.IsSuccess.Should().BeTrue();
        firstPet.Position.Should().Be(Position.Create(5).Value);
        secondPet.Position.Should().Be(Position.Create(1).Value);
        thirdPet.Position.Should().Be(Position.Create(2).Value);
        fourthPet.Position.Should().Be(Position.Create(3).Value);
        fifthPet.Position.Should().Be(Position.Create(4).Value);
    }

    private Volunteer CreateVolunteerWithPet(int petCount)
    {
        var fullName = FullName.Create("Test", "Test", "Test").Value;
        var description = NotEmptyString.Create("TestDescription").Value;
        var phone = ContactPhone.Create("TestPhone").Value;

        var volunteer = new Volunteer(VolunteerId.NewVolunteerId(), fullName, description, 0, phone);

        for (int i = 0; i < petCount; i++)
        {
            var pet = CreatePet();
            volunteer.AddPet(pet);
        }

        return volunteer;
    }

    private Pet CreatePet()
    {
        var nickName = NotEmptyString.Create("TestNick").Value;
        var animalType = NotEmptyString.Create("TestAnimalType").Value;
        var descriptionPet = new CanBeEmptyString("TestDescription");
        var breed = new CanBeEmptyString("breed");
        var color = NotEmptyString.Create("TestColor").Value;
        var healthInformation = new CanBeEmptyString("TestHealthInformation");
        var address = Address.Create("TestAddress", "TestAddress", "TestAddress").Value;
        var phonePet = ContactPhone.Create("TestPhone").Value;

        return new Pet(PetId.NewPetId(),
            nickName,
            animalType,
            descriptionPet,
            breed,
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
            null!);
    }
}