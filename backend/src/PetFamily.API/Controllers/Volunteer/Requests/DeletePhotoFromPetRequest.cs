using PetFamily.Application.VolunteerManagement.Commands.DeletePhoto;

namespace PetFamily.API.Controllers.Volunteer.Requests;

public record DeletePhotoFromPetRequest(Guid PetId, IReadOnlyList<Guid> PhotoIds)
{
    public DeletePhotoFromPetCommand ToCommand(Guid volunteerId) => new(volunteerId, PetId, PhotoIds);
}