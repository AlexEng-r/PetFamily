using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.ValueObjects.String;

public record NotEmptyString
{
    public string Value { get; }

    private NotEmptyString(string value)
    {
        Value = value;
    }

    public static Result<NotEmptyString, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Errors.General.ValueIsInvalid("Value can`t be empty");
        }

        return new NotEmptyString(value);
    }
}