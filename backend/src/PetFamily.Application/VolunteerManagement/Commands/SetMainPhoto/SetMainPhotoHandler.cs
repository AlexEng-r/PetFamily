using CSharpFunctionalExtensions;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Repositories.Volunteers;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.VolunteerManagement.Commands.SetMainPhoto;

public class SetMainPhotoHandler
    : ICommandHandler<SetMainPhotoCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SetMainPhotoHandler(IVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork)
    {
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
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

        await _unitOfWork.SaveChanges(cancellationToken);

        return Result.Success<ErrorList>();
    }
}