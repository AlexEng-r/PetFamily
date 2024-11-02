/*using FluentAssertions;
using PetFamily.Base.Test.Builder;
using PetFamily.Domain.ValueObjects.Positions;

namespace PetFamily.UnitTest;

public class PetTests
{
    [Fact]
    public void Add_Pet_With_Empty_Pet_Return_Success()
    {
        //arrange
        var volunteer = VolunteerBuilder.GetVolunteerWithPets(0);
        var pet = PetBuilder.Create();

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

        var volunteer = VolunteerBuilder.GetVolunteerWithPets(PET_COUNT);
        var petToAdd = PetBuilder.Create();

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

        var volunteer = VolunteerBuilder.GetVolunteerWithPets(PET_COUNT);
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

        var volunteer = VolunteerBuilder.GetVolunteerWithPets(PET_COUNT);
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

        var volunteer = VolunteerBuilder.GetVolunteerWithPets(PET_COUNT);
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

        var volunteer = VolunteerBuilder.GetVolunteerWithPets(PET_COUNT);
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

        var volunteer = VolunteerBuilder.GetVolunteerWithPets(PET_COUNT);
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
}*/