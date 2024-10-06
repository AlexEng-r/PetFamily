using PetFamily.Domain.Shared.Entities.BaseDomain;
using PetFamily.Domain.SpeciesManagement.Breeds;
using PetFamily.Domain.ValueObjects.String;

namespace PetFamily.Domain.SpeciesManagement.Specieses;

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

    public Species(SpeciesId id, NotEmptyString name)
        : base(id)
    {
        Name = name;
    }

    public Species AddBreed(Breed breed)
    {
        _breeds.Add(breed);

        return this;
    }
}