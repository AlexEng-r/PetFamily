using PetFamily.Core.Dtos;
using PetFamily.VolunteerManagement.Application.Commands.UpdateSocialNetworks;

namespace PetFamily.VolunteerManagement.Presentation.Requests;

public record UpdateSocialNetworkRequest(IReadOnlyList<SocialNetworksDto> SocialNetworks)
{
    public UpdateSocialNetworkCommand ToCommand(Guid volunteerId) => new(volunteerId, SocialNetworks);
}