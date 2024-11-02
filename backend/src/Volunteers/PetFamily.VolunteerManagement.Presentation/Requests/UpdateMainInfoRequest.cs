using PetFamily.Core.Dtos;
using PetFamily.VolunteerManagement.Application.Commands.UpdateMainInfo;

namespace PetFamily.VolunteerManagement.Presentation.Requests;

public record UpdateMainInfoRequest(
    FullNameDto FullName,
    string Description,
    int Experience,
    string Phone)
{
    public UpdateMainInfoCommand ToCommand(Guid volunteerId) =>
        new(volunteerId, FullName, Description, Experience, Phone);
}