namespace PetFamily.Application.Volunteers.UpdateSocialNetworks;

public record UpdateSocialNetworkRequest(Guid VolunteerId, UpdateSocialNetworkDto Dto);