using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.Core.MessageQueues;
using PetFamily.Core.Providers.Crypto;
using PetFamily.Core.Providers.File;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects.Ids;
using PetFamily.SharedKernel.ValueObjects.String;
using PetFamily.VolunteerManagement.Application.Repositories;
using PetFamily.VolunteerManagement.Domain.Entities.PetPhotos;
using FileInfo = PetFamily.Core.Providers.File.FileInfo;

namespace PetFamily.VolunteerManagement.Application.Commands.AddPetPhoto;

public class AddPetPhotoHandler
    : ICommandHandler<AddPetPhotoCommand, AddPetPhotoOutputDto>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IFileProvider _fileProvider;
    private readonly IVolunteerUnitOfWork _volunteerUnitOfWork;
    private readonly ILogger<AddPetPhotoHandler> _logger;
    private readonly IValidator<AddPetPhotoCommand> _validator;
    private readonly IMessageQueue<IEnumerable<FileInfo>> _messageQueue;
    private readonly ICryptoProvider _cryptoProvider;

    public AddPetPhotoHandler(IVolunteersRepository volunteersRepository,
        IVolunteerUnitOfWork volunteerUnitOfWork,
        ILogger<AddPetPhotoHandler> logger,
        IFileProvider fileProvider,
        IValidator<AddPetPhotoCommand> validator,
        IMessageQueue<IEnumerable<FileInfo>> messageQueue,
        ICryptoProvider cryptoProvider)
    {
        _volunteersRepository = volunteersRepository;
        _volunteerUnitOfWork = volunteerUnitOfWork;
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

        await _volunteerUnitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Added photo for pet {pet.Id}", pet.Id);

        return new AddPetPhotoOutputDto(pet.Id.Value, failedFiles);
    }
}