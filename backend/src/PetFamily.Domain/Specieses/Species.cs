using PetFamily.Domain.Breeds;
using PetFamily.Domain.SeedWork.Entities.BaseDomain;
using PetFamily.Domain.String;

namespace PetFamily.Domain.Specieses;

public class Species
    : Entity<SpeciesId>
{
    public NotEmptyString Name { get; private set; }

    private readonly List<Breed> _breeds = [];

    public IReadOnlyList<Breed> Breeds => _breeds.AsReadOnly();

    private Species(SpeciesId id)
        : base(id)
    {
    }
}