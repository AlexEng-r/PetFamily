using PetFamily.Application.Volunteers.Common;
using PetFamily.Application.Volunteers.UpdateRequisites;

namespace PetFamily.API.Controllers.Volunteer.Requests;

public record UpdateRequisiteRequest(IReadOnlyList<RequisiteDto> Requisites)
{
    public UpdateRequisiteCommand ToCommand(Guid volunteerId) => new(volunteerId, Requisites);
}