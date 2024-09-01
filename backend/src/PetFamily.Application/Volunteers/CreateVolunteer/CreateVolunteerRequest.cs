using PetFamily.Application.Volunteers.Common;

namespace PetFamily.Application.Volunteers.CreateVolunteer;

public record CreateVolunteerRequest(
    FullNameDto FullName,
    string Description,
    int Experience,
    string Phone,
    IReadOnlyList<CreateRequisitesRequest>? Requisites,
    IReadOnlyList<CreateSocialNetworksRequest>? SocialNetworks);