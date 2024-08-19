namespace PetFamily.Application.Volunteers.CreateVolunteer;

public record CreateVolunteerRequest(
    string FirstName,
    string Surname,
    string Patronymic,
    string Description,
    int Experience,
    string Phone,
    IReadOnlyList<CreateRequisitesRequest>? Requisites,
    IReadOnlyList<CreateSocialNetworksRequest>? SocialNetworks);