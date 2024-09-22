using PetFamily.Application.Volunteers.ChangePetPosition;

namespace PetFamily.API.Controllers.Volunteer.Requests;

public record ChangePetPositionRequest(int PetPosition)
{
    public ChangePetPositionCommand ToCommand(Guid volunteerId, Guid petId)
        => new(volunteerId, petId, PetPosition);
}