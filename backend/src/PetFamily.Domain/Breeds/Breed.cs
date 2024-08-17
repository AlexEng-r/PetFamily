using PetFamily.Domain.SeedWork.Entities.BaseDomain;
using PetFamily.Domain.String;

namespace PetFamily.Domain.Breeds;

public class Breed
    : Entity<BreedId>
{
    public NotEmptyString Name { get; }

    private Breed(BreedId id)
        : base(id)
    {
    }
}