
using PetFamily.SharedKernel.Entities.BaseDomain;
using PetFamily.SharedKernel.Interfaces;
using PetFamily.SharedKernel.ValueObjects.Ids;
using PetFamily.SharedKernel.ValueObjects.String;

namespace PetFamily.SpeciesManagement.Domain.Entities.Breeds;

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