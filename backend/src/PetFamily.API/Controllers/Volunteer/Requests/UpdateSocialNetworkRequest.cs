using PetFamily.Application.Dtos;
using PetFamily.Application.VolunteerManagement.Commands.UpdateSocialNetworks;

namespace PetFamily.API.Controllers.Volunteer.Requests;

public record UpdateSocialNetworkRequest(IReadOnlyList<SocialNetworksDto> SocialNetworks)
{
    public UpdateSocialNetworkCommand ToCommand(Guid volunteerId) => new(volunteerId, SocialNetworks);
}