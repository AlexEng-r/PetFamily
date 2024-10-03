using CSharpFunctionalExtensions;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Application.Repositories.Volunteers;
using PetFamily.Application.VolunteerManagement.Commands.AddPets;
using PetFamily.Base.Test.Builder;
using PetFamily.Domain.Shared;
using PetFamily.Domain.VolunteerManagement.Volunteers;

namespace PetFamily.Application.UnitTest;

public class AddPetTests
{
    private readonly Mock<IVolunteersRepository> _volunteerRepositoryMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IValidator<AddPetCommand>> _validatorMock = new();
    private readonly Mock<ILogger<AddPetHandler>> _loggerMock = new();

    [Fact]
    public async Task AddPetHandler_ShouldAddPet_WhenCommandIsValid()
    {
        // arrange
        var cancellationToken = new CancellationToken();
        var volunteer = VolunteerBuilder.GetVolunteerWithPets(0);

        var addedPet = PetBuilder.Create();
        var addressDto = new AddressDto(addedPet.Address.City, addedPet.Address.House, addedPet.Address.Flat);
        var requisiteDto = addedPet.Requisites.Select(x => new RequisiteDto(x.Name, x.Description)).ToList();
        var command = new AddPetCommand(
            volunteer.Id,
            addedPet.NickName.Value,
            addedPet.AnimalType.Value,
            addedPet.Description.Value,
            addedPet.Breed.Value,
            addedPet.Color.Value,
            addedPet.HealthInformation.Value,
            addressDto,
            addedPet.Weight,
            addedPet.Height,
            addedPet.Phone.Phone,
            addedPet.IsSterialized,
            addedPet.BirthDayDate,
            addedPet.IsVaccinated,
            addedPet.Status,
            requisiteDto);

        _volunteerRepositoryMock.Setup(v => v.GetById(volunteer.Id, cancellationToken))
            .ReturnsAsync(Result.Success<Volunteer, Error>(volunteer));

        _validatorMock.Setup(v => v.ValidateAsync(command, cancellationToken))
            .ReturnsAsync(new ValidationResult());

        _unitOfWorkMock.Setup(x => x.SaveChanges(cancellationToken))
            .Returns(Task.CompletedTask);

        var handler = new AddPetHandler(
            _volunteerRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _loggerMock.Object,
            _validatorMock.Object);

        // act
        var result = await handler.Handle(command, cancellationToken);

        // assert
        result.IsSuccess.Should().BeTrue();
        volunteer.Pets.Should().HaveCount(1);
    }

    [Fact]
    public async Task AddPetHandler_ShouldReturnValidationErrors_WhenCommandIsInvalid()
    {
        // arrange
        var cancellationToken = new CancellationToken();

        var volunteer = VolunteerBuilder.GetVolunteerWithPets(0);
        var addedPet = PetBuilder.Create();
        var addressDto = new AddressDto(addedPet.Address.City, addedPet.Address.House, addedPet.Address.Flat);
        var requisiteDto = addedPet.Requisites.Select(x => new RequisiteDto(x.Name, x.Description)).ToList();
        var command = new AddPetCommand(
            volunteer.Id,
            addedPet.NickName.Value,
            addedPet.AnimalType.Value,
            addedPet.Description.Value,
            addedPet.Breed.Value,
            addedPet.Color.Value,
            addedPet.HealthInformation.Value,
            addressDto,
            addedPet.Weight,
            addedPet.Height,
            addedPet.Phone.Phone,
            addedPet.IsSterialized,
            addedPet.BirthDayDate,
            addedPet.IsVaccinated,
            addedPet.Status,
            requisiteDto);

        var errorValidate = Errors.General.ValueIsInvalid("PhoneNumber").Serialize();
        var validationFailures = new List<ValidationFailure>
        {
            new("PhoneNumber", errorValidate),
        };
        var validationResult = new ValidationResult(validationFailures);

        _validatorMock.Setup(v => v.ValidateAsync(command, cancellationToken))
            .ReturnsAsync(validationResult);

        var handler = new AddPetHandler(
            _volunteerRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _loggerMock.Object,
            _validatorMock.Object);

        // act
        var result = await handler.Handle(command, cancellationToken);

        // assert
        result.IsFailure.Should().BeTrue();
        result.Error.First().InvalidField.Should().Be("PhoneNumber");
    }
}