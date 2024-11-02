using PetFamily.VolunteerManagement.Application.Commands.SetMainPhoto;

namespace PetFamily.VolunteerManagement.Presentation.Requests;

public record SetMainPhotoRequest(Guid PetId, string MainPhotoPath)
{
    public SetMainPhotoCommand ToCommand(Guid volunteerId)
        => new(volunteerId, PetId, MainPhotoPath);
}