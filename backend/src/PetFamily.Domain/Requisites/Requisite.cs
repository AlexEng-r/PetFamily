namespace PetFamily.Domain.Requisites;

public record Requisite
{
    public string Name { get; private set; }

    public string Description { get; private set;}
}