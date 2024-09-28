using PetFamily.Application.Dtos;
using PetFamily.Application.VolunteerManagement.Commands.UpdateMainInfo;

namespace PetFamily.API.Controllers.Volunteer.Requests;

public record UpdateMainInfoRequest(
    FullNameDto FullName,
    string Description,
    int Experience,
    string Phone)
{
    public UpdateMainInfoCommand ToCommand(Guid volunteerId) =>
        new(volunteerId, FullName, Description, Experience, Phone);
}