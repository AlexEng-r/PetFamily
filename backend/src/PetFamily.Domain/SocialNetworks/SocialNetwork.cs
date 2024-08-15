namespace PetFamily.Domain.SocialNetworks;

public record SocialNetwork
{
    public string Name { get; }

    public string Link { get; }

    public SocialNetwork(string name, string link)
    {
        Name = name;
        Link = link;
    }
}