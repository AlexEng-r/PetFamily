using CSharpFunctionalExtensions;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Providers.File;
using PetFamily.Application.Repositories.Volunteers;
using PetFamily.Domain.Shared;
using FileInfo = PetFamily.Application.Providers.File.FileInfo;

namespace PetFamily.Application.VolunteerManagement.Commands.DeletePet;

public class DeletePetHandler
    : ICommandHandler<DeletePetCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileProvider _fileProvider;

    public DeletePetHandler(IVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork,
        IFileProvider fileProvider)
    {
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _fileProvider = fileProvider;
    }

    public async Task<UnitResult<ErrorList>> Handle(DeletePetCommand command, CancellationToken cancellationToken)
    {
        var volunteer = await _volunteersRepository.GetById(command.VolunteerId, cancellationToken);
        if (volunteer.IsFailure)
        {
            return volunteer.Error.ToErrorList();
        }

        var pet = volunteer.Value.Pets.FirstOrDefault(x => x.Id.Value == command.PetId);
        if (pet == null)
        {
            return Errors.General.NotFound(command.PetId).ToErrorList();
        }

        pet.Delete();

        foreach (var petPhoto in pet.PetPhotos)
        {
            var result = await _fileProvider
                .DeleteFileAsync(new FileInfo(petPhoto.Path.Value, petPhoto.BucketName), cancellationToken);
            if (result.IsFailure && result.Error.Type != ErrorType.NotFound)
            {
                return result.Error.ToErrorList();
            }

            await _volunteersRepository.DeletePhotoFromPet(petPhoto, cancellationToken);
        }

        await _unitOfWork.SaveChanges(cancellationToken);

        return Result.Success<ErrorList>();
    }
}