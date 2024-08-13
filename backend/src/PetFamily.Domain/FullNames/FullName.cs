namespace PetFamily.Domain.Fullname;

public record FullName
{
    public string FirstName { get; private set; }

    public string Surname { get; private set; }

    public string Patronymic { get; private set; }
}