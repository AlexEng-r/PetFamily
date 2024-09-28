using PetFamily.Application.Abstractions;
using PetFamily.Application.Dtos;

namespace PetFamily.Application.VolunteerManagement.Commands.UpdateSocialNetworks;

public record UpdateSocialNetworkCommand(Guid VolunteerId, IReadOnlyList<SocialNetworksDto> SocialNetworks)
        : ICommand;