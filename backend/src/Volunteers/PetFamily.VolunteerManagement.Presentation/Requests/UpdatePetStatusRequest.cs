using PetFamily.VolunteerManagement.Application.Commands.UpdatePetStatus;
using PetFamily.VolunteerManagement.Domain.Enums;

namespace PetFamily.VolunteerManagement.Presentation.Requests;

public record UpdatePetStatusRequest(Guid PetId, StatusType Status)
{
    public UpdatePetStatusCommand ToCommand(Guid volunteerId)
        => new(volunteerId, PetId, Status);
}