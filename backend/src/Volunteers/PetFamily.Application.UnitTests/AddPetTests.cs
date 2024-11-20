using CSharpFunctionalExtensions;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;
using PetFamily.Base.Test.Builder;
using PetFamily.Core.Dtos;
using PetFamily.SharedKernel;
using PetFamily.SpeciesManagement.Contracts;
using PetFamily.VolunteerManagement.Application;
using PetFamily.VolunteerManagement.Application.Commands.AddPets;
using PetFamily.VolunteerManagement.Application.Repositories;
using PetFamily.VolunteerManagement.Domain;

namespace PetFamily.Application.UnitTests;

public class AddPetTests
{
    private readonly Mock<IVolunteersRepository> _volunteerRepositoryMock = new();
    private readonly Mock<IVolunteerUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IValidator<AddPetCommand>> _validatorMock = new();
    private readonly Mock<ILogger<AddPetHandler>> _loggerMock = new();
    private readonly Mock<ISpeciesContract> _speciesContractMock = new();

    [Fact]
    public async Task AddPetHandler_ShouldAddPet_WhenCommandIsValid()
    {
        // arrange
        var cancellationToken = new CancellationToken();
        var volunteer = VolunteerBuilder.GetVolunteerWithPets(0);

        var addedPet = PetBuilder.Create();
        var addressDto = new AddressDto(addedPet.Address.City, addedPet.Address.House, addedPet.Address.Flat);
        var speciesDto = CreateSpeciesWithBreeds();
        var requisiteDto = addedPet.Requisites.Select(x => new RequisiteDto(x.Name, x.Description)).ToList();
        var command = new AddPetCommand(
            volunteer.Id,
            addedPet.NickName.Value,
            addedPet.AnimalType.Value,
            addedPet.Description.Value,
            speciesDto.Id,
            speciesDto.Breeds.First().Id,
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

        _speciesContractMock.Setup(v => v.GetSpeciesById(command.SpeciesId!.Value, cancellationToken))
            .ReturnsAsync(Result.Success<SpeciesDto, Error>(speciesDto));

        _validatorMock.Setup(v => v.ValidateAsync(command, cancellationToken))
            .ReturnsAsync(new ValidationResult());

        _unitOfWorkMock.Setup(x => x.SaveChanges(cancellationToken))
            .Returns(Task.CompletedTask);

        var handler = new AddPetHandler(
            _volunteerRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _loggerMock.Object,
            _validatorMock.Object,
            _speciesContractMock.Object);

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
            Guid.NewGuid(),
            Guid.NewGuid(),
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
            _validatorMock.Object,
            _speciesContractMock.Object);

        // act
        var result = await handler.Handle(command, cancellationToken);

        // assert
        result.IsFailure.Should().BeTrue();
        result.Error.First().InvalidField.Should().Be("PhoneNumber");
    }

    private SpeciesDto CreateSpeciesWithBreeds()
    {
        var speciesId = Guid.NewGuid();
        return new SpeciesDto
        {
            Id = speciesId, Name = "SpeciesName",
            Breeds = [new BreedDto { Id = new Guid(), SpeciesId = speciesId, Name = "BreedName" }]
        };
    }
}