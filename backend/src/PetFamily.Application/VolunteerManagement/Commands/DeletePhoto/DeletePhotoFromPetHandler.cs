using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Application.Providers.File;
using PetFamily.Application.Repositories.Volunteers;
using PetFamily.Domain.Shared;
using PetFamily.Domain.VolunteerManagement.Pets;
using FileInfo = PetFamily.Application.Providers.File.FileInfo;

namespace PetFamily.Application.VolunteerManagement.Commands.DeletePhoto;

public class DeletePhotoFromPetHandler
    : ICommandHandler<DeletePhotoFromPetCommand>
{
    private readonly IValidator<DeletePhotoFromPetCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileProvider _fileProvider;

    public DeletePhotoFromPetHandler(IValidator<DeletePhotoFromPetCommand> validator,
        IVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork,
        IFileProvider fileProvider)
    {
        _validator = validator;
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _fileProvider = fileProvider;
    }

    public async Task<UnitResult<ErrorList>> Handle(DeletePhotoFromPetCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }

        var volunteer = await _volunteersRepository.GetById(command.VolunteerId, cancellationToken);
        if (volunteer.IsFailure)
        {
            return Errors.General.NotFound(command.VolunteerId).ToErrorList();
        }

        var petId = PetId.Create(command.PetId);

        var pet = volunteer.Value.Pets.FirstOrDefault(x => x.Id == petId);
        if (pet == null)
        {
            return Errors.General.NotFound(command.PetId).ToErrorList();
        }

        foreach (var photoId in command.PhotoIds)
        {
            var petPhoto = pet.PetPhotos.First(x => x.Id.Value == photoId);

            var result = await _fileProvider
                .DeleteFileAsync(new FileInfo(petPhoto.Path.Value, petPhoto.BucketName), cancellationToken);

            if (result.IsSuccess)
            {
                await _volunteersRepository.DeletePhotoFromPet(petPhoto, cancellationToken);
            }
        }

        await _unitOfWork.SaveChanges(cancellationToken);

        return UnitResult.Success<ErrorList>();
    }
}