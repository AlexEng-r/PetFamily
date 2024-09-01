namespace PetFamily.Domain.SocialNetworks;

public record SocialNetworkDetails
{
    public IReadOnlyList<SocialNetwork> SocialNetworks { get; }

    private SocialNetworkDetails()
    {
    }

    public SocialNetworkDetails(IReadOnlyList<SocialNetwork> socialNetworks)
    {
        SocialNetworks = socialNetworks;
    }
}