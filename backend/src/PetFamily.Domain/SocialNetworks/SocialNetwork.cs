namespace PetFamily.Domain.SocialNetworks;

public record SocialNetwork
{
    public string Name { get; private set; }

    public string Link { get; private set; }
}