using PetFamily.Application.Volunteers.Common;

namespace PetFamily.Application.Volunteers.UpdateSocialNetworks;

public record UpdateSocialNetworkDto(IReadOnlyList<SocialNetworksDto> SocialNetworks);