namespace PetFamily.Domain.ValueObjects.String;

public record CanBeEmptyString
{
    public string? Value { get; }

    public CanBeEmptyString(string? value)
    {
        Value = value;
    }
}