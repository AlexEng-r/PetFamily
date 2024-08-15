namespace PetFamily.Domain.Contacts;

public record ContactPhone
{
    public string Value { get; }

    public ContactPhone(string value)
    {
        Value = value;
    }
}