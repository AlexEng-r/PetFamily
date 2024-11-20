using PetFamily.VolunteerManagement.Application.Commands.ChangePetPosition;

namespace PetFamily.VolunteerManagement.Presentation.Requests;

public record ChangePetPositionRequest(int PetPosition)
{
    public ChangePetPositionCommand ToCommand(Guid volunteerId, Guid petId)
        => new(volunteerId, petId, PetPosition);
}