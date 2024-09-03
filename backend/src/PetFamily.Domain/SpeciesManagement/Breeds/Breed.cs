using PetFamily.Domain.Shared.Entities.BaseDomain;
using PetFamily.Domain.ValueObjects.String;

namespace PetFamily.Domain.SpeciesManagement.Breeds;

public class Breed
    : Entity<BreedId>
{
    public NotEmptyString Name { get; }

    private Breed(BreedId id)
        : base(id)
    {
    }
}