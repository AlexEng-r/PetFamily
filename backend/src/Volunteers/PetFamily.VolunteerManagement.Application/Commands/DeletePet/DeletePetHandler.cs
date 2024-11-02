using CSharpFunctionalExtensions;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Providers.File;
using PetFamily.SharedKernel;
using PetFamily.VolunteerManagement.Application.Repositories;
using FileInfo = PetFamily.Core.Providers.File.FileInfo;

namespace PetFamily.VolunteerManagement.Application.Commands.DeletePet;

public class DeletePetHandler
    : ICommandHandler<DeletePetCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IVolunteerUnitOfWork _volunteerUnitOfWork;
    private readonly IFileProvider _fileProvider;

    public DeletePetHandler(IVolunteersRepository volunteersRepository,
        IVolunteerUnitOfWork volunteerUnitOfWork,
        IFileProvider fileProvider)
    {
        _volunteersRepository = volunteersRepository;
        _volunteerUnitOfWork = volunteerUnitOfWork;
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

        await _volunteerUnitOfWork.SaveChanges(cancellationToken);

        return Result.Success<ErrorList>();
    }
}