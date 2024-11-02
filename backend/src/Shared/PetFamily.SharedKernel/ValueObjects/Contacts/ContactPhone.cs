using CSharpFunctionalExtensions;

namespace PetFamily.SharedKernel.ValueObjects.Contacts;

public record ContactPhone
{
    public string Phone { get; }

    private ContactPhone(string phone)
    {
        Phone = phone;
    }

    public static Result<ContactPhone, Error> Create(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone) || phone.Length > ConfigurationConstraint.MIN20_TEXT_LENGTH)
        {
            return Errors.General.ValueIsInvalid("Phone");
        }

        return new ContactPhone(phone);
    }
}