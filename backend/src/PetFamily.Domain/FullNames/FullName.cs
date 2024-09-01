using CSharpFunctionalExtensions;
using PetFamily.Domain.SeedWork;

namespace PetFamily.Domain.FullNames;

public record FullName
{
    public string FirstName { get; }

    public string Surname { get; }

    public string? Patronymic { get; }

    private FullName(string firstName, string surname, string? patronymic)
    {
        FirstName = firstName;
        Surname = surname;
        Patronymic = patronymic;
    }

    public static Result<FullName, Error> Create(string firstName, string surname, string? patronymic)
    {
        if (string.IsNullOrWhiteSpace(firstName) || firstName.Length > ConfigurationConstraint.MIN20_TEXT_LENGTH)
        {
            return Errors.General.ValueIsInvalid("Firstname");
        }

        if (string.IsNullOrWhiteSpace(surname))
        {
            return Errors.General.ValueIsInvalid("Surname");
        }

        if (patronymic != null && patronymic.Length > ConfigurationConstraint.MIN20_TEXT_LENGTH)
        {
            return Errors.General.ValueIsInvalid("Patronymic");
        }

        return new FullName(firstName, surname, patronymic);
    }
}