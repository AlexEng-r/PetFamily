using PetFamily.Core.Dtos;
using PetFamily.VolunteerManagement.Application.Commands.UpdateRequisites;

namespace PetFamily.VolunteerManagement.Presentation.Requests;

public record UpdateRequisiteRequest(IReadOnlyList<RequisiteDto> Requisites)
{
    public UpdateRequisiteCommand ToCommand(Guid volunteerId) => new(volunteerId, Requisites);
}