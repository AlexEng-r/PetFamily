using CSharpFunctionalExtensions;
using PetFamily.Domain.SeedWork;

namespace PetFamily.Domain.SocialNetworks;

public record SocialNetwork
{
    public string Name { get; }

    public string Link { get; }

    private SocialNetwork(string name, string link)
    {
        Name = name;
        Link = link;
    }

    public static Result<SocialNetwork, Error> Create(string name, string link)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > ConfigurationConstraint.MIN20_TEXT_LENGTH)
        {
            return Errors.General.ValueIsInvalid("Social network");
        }

        if (string.IsNullOrWhiteSpace(link) || name.Length > ConfigurationConstraint.AVERAGE_TEXT_LENGTH)
        {
            return Errors.General.ValueIsInvalid("Social network link");
        }

        return new SocialNetwork(name, link);
    }
}