namespace PetFamily.Domain.String;

public record NotEmptyString
{
    public string Value { get; }

    private NotEmptyString(string value)
    {
        Value = value;
    }
}