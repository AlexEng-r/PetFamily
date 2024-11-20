using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos;

namespace PetFamily.VolunteerManagement.Application.Commands.Create;

public record CreateVolunteerCommand(
    FullNameDto FullName,
    string Description,
    int Experience,
    string Phone,
    IReadOnlyList<RequisiteDto>? Requisites,
    IReadOnlyList<SocialNetworksDto>? SocialNetworks)
        : ICommand;