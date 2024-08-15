namespace PetFamily.Domain.Requisites;

public record Requisite
{
    public string Name { get; }

    public string Description { get; }

    public Requisite(string name, string description)
    {
        Name = name;
        Description = description;
    }
}