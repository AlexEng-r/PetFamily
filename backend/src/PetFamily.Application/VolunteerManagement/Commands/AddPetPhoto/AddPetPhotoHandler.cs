using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Application.MessageQueues;
using PetFamily.Application.Providers.Crypto;
using PetFamily.Application.Providers.File;
using PetFamily.Application.Repositories.Volunteers;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects.String;
using PetFamily.Domain.VolunteerManagement.PetPhotos;
using PetFamily.Domain.VolunteerManagement.Pets;
using PetFamily.Domain.VolunteerManagement.Volunteers;
using FileInfo = PetFamily.Application.Providers.File.FileInfo;

namespace PetFamily.Application.VolunteerManagement.Commands.AddPetPhoto;

public class AddPetPhotoHandler
    : ICommandHandler<AddPetPhotoCommand, AddPetPhotoOutputDto>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IFileProvider _fileProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddPetPhotoHandler> _logger;
    private readonly IValidator<AddPetPhotoCommand> _validator;
    private readonly IMessageQueue<IEnumerable<FileInfo>> _messageQueue;
    private readonly ICryptoProvider _cryptoProvider;

    public AddPetPhotoHandler(IVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork,
        ILogger<AddPetPhotoHandler> logger,
        IFileProvider fileProvider,
        IValidator<AddPetPhotoCommand> validator,
        IMessageQueue<IEnumerable<FileInfo>> messageQueue,
        ICryptoProvider cryptoProvider)
    {
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _fileProvider = fileProvider;
        _validator = validator;
        _messageQueue = messageQueue;
        _cryptoProvider = cryptoProvider;
    }

    public async Task<Result<AddPetPhotoOutputDto, ErrorList>> Handle(AddPetPhotoCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }

        var volunteerId = VolunteerId.Create(command.VolunteerId);
        var volunteer = await _volunteersRepository.GetById(volunteerId, cancellationToken);
        if (volunteer.IsFailure)
        {
            return volunteer.Error.ToErrorList();
        }

        var petId = PetId.Create(command.PetId);
        var pet = volunteer.Value.GetPetById(petId);
        if (pet == null)
        {
            return Errors.General.NotFound(petId.Value).ToErrorList();
        }

        var fileData = new List<FileData>();
        var failedFiles = new List<string>();
        foreach (var file in command.Files)
        {
            var hashCode = Convert.ToBase64String(_cryptoProvider.Sha256(file.Stream));
            if (pet.PetPhotos.Any(x => x.HashCode == hashCode))
            {
                failedFiles.Add(file.ObjectName);
            }
            else
            {
                file.Stream.Seek(0, SeekOrigin.Begin);
                var fileInfo = new FileInfo(Guid.NewGuid() + Path.GetExtension(file.ObjectName), command.BucketName);
                fileData.Add(new FileData(file.Stream, hashCode, fileInfo));
            }
        }

        var pathResult = await _fileProvider.UploadFilesAsync(fileData, cancellationToken);
        if (pathResult.IsFailure)
        {
            await _messageQueue.WriteAsync(fileData.Select(x => x.FileInfo), cancellationToken);

            return pathResult.Error.ToErrorList();
        }

        var petPhotos = pathResult.Value.Select(x
            => new PetPhoto(
                PetPhotoId.NewPetPhotoId(),
                NotEmptyString.Create(x.Path).Value,
                x.HashCode,
                isMain: false,
                command.BucketName));

        pet.AddPetPhotos(petPhotos);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Added photo for pet {pet.Id}", pet.Id);

        return new AddPetPhotoOutputDto(pet.Id.Value, failedFiles);
    }
}