using CSharpFunctionalExtensions;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;
using PetFamily.Base.Test.Builder;
using PetFamily.Core.Dtos;
using PetFamily.Core.MessageQueues;
using PetFamily.Core.Providers.Crypto;
using PetFamily.Core.Providers.File;
using PetFamily.SharedKernel;
using PetFamily.VolunteerManagement.Application;
using PetFamily.VolunteerManagement.Application.Commands.AddPetPhoto;
using PetFamily.VolunteerManagement.Application.Repositories;
using PetFamily.VolunteerManagement.Domain;
using FileInfo = PetFamily.Core.Providers.File.FileInfo;

namespace PetFamily.Application.UnitTests;

public class UploadPhotoToPetTests
{
    private readonly Mock<IVolunteersRepository> _volunteerRepositoryMock = new();
    private readonly Mock<IValidator<AddPetPhotoCommand>> _validatorMock = new();
    private readonly Mock<IVolunteerUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<ILogger<AddPetPhotoHandler>> _loggerMock = new();
    private readonly Mock<IFileProvider> _fileProviderMock = new();
    private readonly Mock<IMessageQueue<IEnumerable<FileInfo>>> _messageQueueMock = new();
    private readonly Mock<ICryptoProvider> _cryptoProviderMock = new();

    private const string BUCKET_NAME = "TestBucket";

    [Fact]
    public async void UploadPhotoHandler_ShouldUploadFilesToPet()
    {
        // arrange
        var cancellationToken = new CancellationToken();

        var volunteer = VolunteerBuilder.GetVolunteerWithPets(1);
        var volunteerId = volunteer.Id;
        var petId = volunteer.Pets[0].Id;

        var outputDto = new AddPetPhotoOutputDto(petId, new List<string>());

        var uploadFileDto = new List<UploadFileDto>
        {
            new(new MemoryStream(), Guid.NewGuid() + ".png"),
            new(new MemoryStream(), Guid.NewGuid() + ".png")
        };

        var filePath = uploadFileDto.Select(x => x.ObjectName).ToList();
        var outputFileDto = filePath.Select(x => new UploadFileProviderOutputDto(x, "123")).ToList();

        var command = new AddPetPhotoCommand(volunteerId, petId, BUCKET_NAME, uploadFileDto);

        _volunteerRepositoryMock.Setup(x => x.GetById(volunteerId, cancellationToken))
            .ReturnsAsync(Result.Success<Volunteer, Error>(volunteer));

        _unitOfWorkMock.Setup(x => x.SaveChanges(cancellationToken))
            .Returns(Task.CompletedTask);

        _validatorMock.Setup(x => x.ValidateAsync(command, cancellationToken))
            .ReturnsAsync(new ValidationResult());

        _fileProviderMock.Setup(x => x.UploadFilesAsync(It.IsAny<IEnumerable<FileData>>(), cancellationToken))
            .ReturnsAsync(Result.Success<IReadOnlyList<UploadFileProviderOutputDto>, Error>(outputFileDto));

        _messageQueueMock.Setup(x => x.WriteAsync(filePath.Select(v => new FileInfo(v, "photos")), cancellationToken))
            .Returns(Task.CompletedTask);

        _cryptoProviderMock.Setup(x => x.Sha256(It.IsAny<Stream>())).Returns([byte.MinValue]);

        var handler = new AddPetPhotoHandler(
            _volunteerRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _loggerMock.Object,
            _fileProviderMock.Object,
            _validatorMock.Object,
            _messageQueueMock.Object,
            _cryptoProviderMock.Object);

        // act
        var resultHandle = await handler.Handle(command, cancellationToken);

        // assert
        resultHandle.IsSuccess.Should().BeTrue();
        resultHandle.Value.PetId.Equals(outputDto.PetId).Should().BeTrue();
        volunteer.Pets.First(x => x.Id == petId).PetPhotos.Should().HaveCount(2);
    }

    [Fact]
    public async Task UploadPhotoHandler_NotFoundVolunteer_ShouldReturnNotFoundError()
    {
        // arrange
        var cancellationToken = new CancellationToken();

        var volunteer = VolunteerBuilder.GetVolunteerWithPets(1);
        var volunteerId = volunteer.Id;
        var petId = volunteer.Pets[0].Id;

        var uploadFileDto = new List<UploadFileDto>
        {
            new(new MemoryStream(), Guid.NewGuid() + ".png"),
            new(new MemoryStream(), Guid.NewGuid() + ".png")
        };

        var filePath = uploadFileDto.Select(x => x.ObjectName).ToList();

        var command = new AddPetPhotoCommand(volunteerId, petId, BUCKET_NAME, uploadFileDto);

        _validatorMock.Setup(x => x.ValidateAsync(command, cancellationToken))
            .ReturnsAsync(new ValidationResult());

        _volunteerRepositoryMock.Setup(x => x.GetById(volunteerId, cancellationToken))
            .ReturnsAsync(Result.Failure<Volunteer, Error>(Errors.General.NotFound(volunteerId)));

        _messageQueueMock.Setup(x => x.WriteAsync(filePath.Select(v => new FileInfo(v, "photos")), cancellationToken))
            .Returns(Task.CompletedTask);

        _cryptoProviderMock.Setup(x => x.Sha256(It.IsAny<Stream>())).Returns([byte.MinValue]);

        var handler = new AddPetPhotoHandler(
            _volunteerRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _loggerMock.Object,
            _fileProviderMock.Object,
            _validatorMock.Object,
            _messageQueueMock.Object,
            _cryptoProviderMock.Object);

        // act
        var result = await handler.Handle(command, cancellationToken);

        // assert
        result.IsFailure.Should().BeTrue();
        var error = result.Error.First();
        error.Code.Should().Be("record.not.found");
        error.Message.Should().Contain("record not found for Id");
        error.Type.Should().Be(ErrorType.NotFound);
    }

    [Fact]
    public async Task UploadPhotoHandler_FileUploadFails_ShouldReturnFileUploadError()
    {
        // arrange
        var cancellationToken = new CancellationToken();

        var volunteer = VolunteerBuilder.GetVolunteerWithPets(1);
        var volunteerId = volunteer.Id;
        var petId = volunteer.Pets[0].Id;

        var uploadFileDto = new List<UploadFileDto>
        {
            new(new MemoryStream(), Guid.NewGuid() + ".png"),
            new(new MemoryStream(), Guid.NewGuid() + ".png")
        };

        var command = new AddPetPhotoCommand(volunteerId, petId, BUCKET_NAME, uploadFileDto);

        var filePath = uploadFileDto.Select(x => x.ObjectName).ToList();

        _validatorMock.Setup(v => v.ValidateAsync(command, cancellationToken))
            .ReturnsAsync(new ValidationResult());

        _volunteerRepositoryMock.Setup(v => v.GetById(volunteerId, cancellationToken))
            .ReturnsAsync(Result.Success<Volunteer, Error>(volunteer));

        _fileProviderMock.Setup(x => x.UploadFilesAsync(It.IsAny<IEnumerable<FileData>>(), cancellationToken))
            .ReturnsAsync(Result.Failure<IReadOnlyList<UploadFileProviderOutputDto>, Error>(
                Error.Failure("file.upload", "Fail to upload files in minio")));

        _messageQueueMock.Setup(x => x.WriteAsync(filePath.Select(v => new FileInfo(v, "photos")), cancellationToken))
            .Returns(Task.CompletedTask);

        _cryptoProviderMock.Setup(x => x.Sha256(It.IsAny<Stream>())).Returns([byte.MinValue]);

        var handler = new AddPetPhotoHandler(
            _volunteerRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _loggerMock.Object,
            _fileProviderMock.Object,
            _validatorMock.Object,
            _messageQueueMock.Object,
            _cryptoProviderMock.Object);

        // act
        var result = await handler.Handle(command, cancellationToken);
        // assert
        result.IsFailure.Should().BeTrue();
        var error = result.Error.First();
        error.Code.Should().Be("file.upload");
        error.Message.Should().Contain("Fail to upload files in minio");
        error.Type.Should().Be(ErrorType.Failure);
    }
}