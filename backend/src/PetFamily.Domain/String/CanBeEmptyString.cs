namespace PetFamily.Domain.String;

public record CanBeEmptyString
{
    public string? Value { get; }

    private CanBeEmptyString(string value)
    {
        Value = value;
    }
}