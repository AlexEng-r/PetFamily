using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Application.MessageQueues;
using PetFamily.Application.Providers;
using PetFamily.Application.Repositories.Volunteers;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects.String;
using PetFamily.Domain.VolunteerManagement.PetPhotos;
using PetFamily.Domain.VolunteerManagement.Pets;
using PetFamily.Domain.VolunteerManagement.Volunteers;
using FileInfo = PetFamily.Application.Providers.FileInfo;

namespace PetFamily.Application.Volunteers.AddPetPhoto;

public class AddPetPhotoHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IFileProvider _fileProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddPetPhotoHandler> _logger;
    private readonly IValidator<AddPetPhotoCommand> _validator;
    private readonly IMessageQueue<IEnumerable<FileInfo>> _messageQueue;

    private const string BUCKET_NAME = "photos";

    public AddPetPhotoHandler(IVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork,
        ILogger<AddPetPhotoHandler> logger,
        IFileProvider fileProvider,
        IValidator<AddPetPhotoCommand> validator,
        IMessageQueue<IEnumerable<FileInfo>> messageQueue)
    {
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _fileProvider = fileProvider;
        _validator = validator;
        _messageQueue = messageQueue;
    }

    public async Task<Result<Guid, ErrorList>> Handle(AddPetPhotoCommand command,
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

        var fileData = command.Files
            .Select(x
                => new FileData(x.Stream, new FileInfo(Guid.NewGuid() + Path.GetExtension(x.ObjectName), BUCKET_NAME)))
            .ToList();

        var pathResult = await _fileProvider.UploadFilesAsync(fileData, cancellationToken);
        if (pathResult.IsFailure)
        {
            await _messageQueue.WriteAsync(fileData.Select(x => x.FileInfo), cancellationToken);

            return pathResult.Error.ToErrorList();
        }

        var petPhotos = pathResult.Value.Select(x
            => new PetPhoto(PetPhotoId.NewPetPhotoId(), NotEmptyString.Create(x).Value, isMain: false));

        pet.AddPetPhotos(petPhotos);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Added photo for pet {pet.Id}", pet.Id);

        return pet.Id.Value;
    }
}