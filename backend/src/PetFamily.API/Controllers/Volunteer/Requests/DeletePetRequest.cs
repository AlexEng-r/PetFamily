using PetFamily.Application.VolunteerManagement.Commands.DeletePet;

namespace PetFamily.API.Controllers.Volunteer.Requests;

public record DeletePetRequest(Guid PetId)
{
    public DeletePetCommand ToCommand(Guid volunteerId)
        => new(volunteerId, PetId);
}