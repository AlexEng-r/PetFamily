using PetFamily.Application.Dtos;
using PetFamily.Application.VolunteerManagement.Commands.UpdateRequisites;

namespace PetFamily.API.Controllers.Volunteer.Requests;

public record UpdateRequisiteRequest(IReadOnlyList<RequisiteDto> Requisites)
{
    public UpdateRequisiteCommand ToCommand(Guid volunteerId) => new(volunteerId, Requisites);
}