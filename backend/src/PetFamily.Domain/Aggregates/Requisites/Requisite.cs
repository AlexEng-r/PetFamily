using PetFamily.Domain.SeedWork.Entities;
using PetFamily.Domain.SeedWork.Entities.BaseDomain;

namespace PetFamily.Domain.Aggregates.Requisites;

public class Requisite
    : IValueObject
{
    public string Name { get; }

    public string Description { get; }

    public Requisite(string name, string description)
    {
        Name = name;
        Description = description;
    }
}