using CSharpFunctionalExtensions;
using PetFamily.Core.Abstractions;
using PetFamily.SharedKernel;
using PetFamily.VolunteerManagement.Application.Repositories;

namespace PetFamily.VolunteerManagement.Application.Commands.SetMainPhoto;

public class SetMainPhotoHandler
    : ICommandHandler<SetMainPhotoCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IVolunteerUnitOfWork _volunteerUnitOfWork;

    public SetMainPhotoHandler(IVolunteersRepository volunteersRepository,
        IVolunteerUnitOfWork volunteerUnitOfWork)
    {
        _volunteersRepository = volunteersRepository;
        _volunteerUnitOfWork = volunteerUnitOfWork;
    }

    public async Task<UnitResult<ErrorList>> Handle(SetMainPhotoCommand command, CancellationToken cancellationToken)
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

        var petPhoto = pet.PetPhotos.FirstOrDefault(x => x.Path.Value == command.PhotoPath);
        if (petPhoto == null)
        {
            return Errors.General.NotFound().ToErrorList();
        }

        foreach (var photo in pet.PetPhotos)
        {
            photo.SetMain(false);
        }

        petPhoto.SetMain();

        await _volunteerUnitOfWork.SaveChanges(cancellationToken);

        return Result.Success<ErrorList>();
    }
}