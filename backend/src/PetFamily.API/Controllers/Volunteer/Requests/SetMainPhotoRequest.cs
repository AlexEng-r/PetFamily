using PetFamily.Application.VolunteerManagement.Commands.SetMainPhoto;

namespace PetFamily.API.Controllers.Volunteer.Requests;

public record SetMainPhotoRequest(Guid PetId, string MainPhotoPath)
{
    public SetMainPhotoCommand ToCommand(Guid volunteerId)
        => new(volunteerId, PetId, MainPhotoPath);
}