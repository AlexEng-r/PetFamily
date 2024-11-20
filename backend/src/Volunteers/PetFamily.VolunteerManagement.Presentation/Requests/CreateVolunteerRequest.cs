using PetFamily.Core.Dtos;
using PetFamily.VolunteerManagement.Application.Commands.Create;

namespace PetFamily.VolunteerManagement.Presentation.Requests;

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