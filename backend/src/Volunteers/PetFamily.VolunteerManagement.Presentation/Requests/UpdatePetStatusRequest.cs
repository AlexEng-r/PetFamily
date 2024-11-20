using PetFamily.Core.Enums;
using PetFamily.VolunteerManagement.Application.Commands.UpdatePetStatus;

namespace PetFamily.VolunteerManagement.Presentation.Requests;

public record UpdatePetStatusRequest(Guid PetId, StatusType Status)
{
    public UpdatePetStatusCommand ToCommand(Guid volunteerId)
        => new(volunteerId, PetId, Status);
}