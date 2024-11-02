using PetFamily.VolunteerManagement.Application.Commands.DeletePhoto;

namespace PetFamily.VolunteerManagement.Presentation.Requests;

public record DeletePhotoFromPetRequest(Guid PetId, IReadOnlyList<Guid> PhotoIds)
{
    public DeletePhotoFromPetCommand ToCommand(Guid volunteerId) => new(volunteerId, PetId, PhotoIds);
}