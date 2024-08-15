namespace PetFamily.Domain.FullNames;

public record FullName
{
    public string FirstName { get; }

    public string Surname { get; }

    public string Patronymic { get; }

    public FullName(string firstName, string surname, string patronymic)
    {
        FirstName = firstName;
        Surname = surname;
        Patronymic = patronymic;
    }
}