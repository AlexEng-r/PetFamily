using PetFamily.Domain.Shared.Entities.BaseDomain;
using PetFamily.Domain.Shared.Interfaces;
using PetFamily.Domain.ValueObjects.String;

namespace PetFamily.Domain.SpeciesManagement.Breeds;

public class Breed
    : Entity<BreedId>, ISoftDeletable
{
    public NotEmptyString Name { get; }

    public bool IsDeleted { get; private set; }

    private Breed(BreedId id)
        : base(id)
    {
    }

    public Breed(BreedId id, NotEmptyString name)
        : base(id)
    {
        Name = name;
    }

    public void Delete() => IsDeleted = true;

    public void Restore() => IsDeleted = false;
}