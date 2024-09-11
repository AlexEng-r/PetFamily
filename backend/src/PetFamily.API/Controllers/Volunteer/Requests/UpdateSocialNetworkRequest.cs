using PetFamily.Application.Volunteers.Common;
using PetFamily.Application.Volunteers.UpdateSocialNetworks;

namespace PetFamily.API.Controllers.Volunteer.Requests;

public record UpdateSocialNetworkRequest(IReadOnlyList<SocialNetworksDto> SocialNetworks)
{
    public UpdateSocialNetworkCommand ToCommand(Guid volunteerId) => new(volunteerId, SocialNetworks);
}