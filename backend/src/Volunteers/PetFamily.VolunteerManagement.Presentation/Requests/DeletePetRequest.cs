using PetFamily.VolunteerManagement.Application.Commands.DeletePet;

namespace PetFamily.VolunteerManagement.Presentation.Requests;

public record DeletePetRequest(Guid PetId)
{
    public DeletePetCommand ToCommand(Guid volunteerId)
        => new(volunteerId, PetId);
}