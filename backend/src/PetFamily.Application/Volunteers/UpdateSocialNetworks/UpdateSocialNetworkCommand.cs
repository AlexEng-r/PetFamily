using PetFamily.Application.Volunteers.Common;

namespace PetFamily.Application.Volunteers.UpdateSocialNetworks;

public record UpdateSocialNetworkCommand(Guid VolunteerId, IReadOnlyList<SocialNetworksDto> SocialNetworks);