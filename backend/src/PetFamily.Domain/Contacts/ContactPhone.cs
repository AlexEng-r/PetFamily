using CSharpFunctionalExtensions;
using PetFamily.Domain.SeedWork;

namespace PetFamily.Domain.Contacts;

public record ContactPhone
{
    public string Value { get; }

    private ContactPhone(string value)
    {
        Value = value;
    }

    public static Result<ContactPhone, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > ConfigurationConstraint.MIN20_TEXT_LENGTH)
        {
            return Errors.General.ValueIsInvalid("Phone");
        }

        return new ContactPhone(value);
    }
}