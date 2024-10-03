using System.Text.Json.Serialization;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.ValueObjects.Requisites;

public record Requisite
{
    public string Name { get; }

    public string Description { get; }

    [JsonConstructor]
    private Requisite(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public static Result<Requisite, Error> Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > ConfigurationConstraint.MIN20_TEXT_LENGTH)
        {
            return Errors.General.ValueIsInvalid("Requisite name");
        }

        if (string.IsNullOrWhiteSpace(description) || description.Length > ConfigurationConstraint.AVERAGE_TEXT_LENGTH)
        {
            return Errors.General.ValueIsInvalid("Requisite description");
        }

        return new Requisite(name, description);
    }
}