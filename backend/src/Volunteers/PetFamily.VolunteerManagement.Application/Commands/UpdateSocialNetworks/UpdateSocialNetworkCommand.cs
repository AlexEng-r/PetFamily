using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos;

namespace PetFamily.VolunteerManagement.Application.Commands.UpdateSocialNetworks;

public record UpdateSocialNetworkCommand(Guid VolunteerId, IReadOnlyList<SocialNetworksDto> SocialNetworks)
        : ICommand;