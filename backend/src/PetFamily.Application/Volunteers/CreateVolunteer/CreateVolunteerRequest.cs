using PetFamily.Application.Volunteers.FullNames;

namespace PetFamily.Application.Volunteers.CreateVolunteer;

public record CreateVolunteerRequest(
    FullNameRequest FullName,
    string Description,
    int Experience,
    string Phone,
    IReadOnlyList<CreateRequisitesRequest>? Requisites,
    IReadOnlyList<CreateSocialNetworksRequest>? SocialNetworks);