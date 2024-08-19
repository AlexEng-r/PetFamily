namespace PetFamily.Domain.String;

public record CanBeEmptyString
{
    public string? Value { get; }

    public CanBeEmptyString(string? value)
    {
        Value = value;
    }
}