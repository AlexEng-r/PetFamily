using PetFamily.Application.Volunteers.Common;
using PetFamily.Application.Volunteers.Create;

namespace PetFamily.API.Controllers.Volunteer.Requests;

public record CreateVolunteerRequest(
    FullNameDto FullName,
    string Description,
    int Experience,
    string Phone,
    IReadOnlyList<RequisiteDto>? Requisites,
    IReadOnlyList<SocialNetworksDto>? SocialNetworks)
{
    public CreateVolunteerCommand ToCommand()
        => new(FullName, Description, Experience, Phone, Requisites, SocialNetworks);
}