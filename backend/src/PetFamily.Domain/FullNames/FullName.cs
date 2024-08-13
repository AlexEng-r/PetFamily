namespace PetFamily.Domain.Fullname;

public record FullName
{
    public string FirstName { get; }

    public string Surname { get; }

    public string Patronymic { get; }
}