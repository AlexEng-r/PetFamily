using PetFamily.Application.VolunteerManagement.Commands.UpdatePetStatus;
using PetFamily.Domain.VolunteerManagement.Enums;

namespace PetFamily.API.Controllers.Volunteer.Requests;

public record UpdatePetStatusRequest(Guid PetId, StatusType Status)
{
    public UpdatePetStatusCommand ToCommand(Guid volunteerId)
        => new(volunteerId, PetId, Status);
}