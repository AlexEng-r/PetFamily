using PetFamily.Domain.Breeds;
using PetFamily.Domain.Specieses;

namespace PetFamily.Domain.SpeciesDetails;

public record SpeciesDetail
{
    public SpeciesId SpeciesId { get; }

    public BreedId BreedId { get; }

    private SpeciesDetail(SpeciesId speciesId, BreedId breedId)
    {
        SpeciesId = speciesId;
        BreedId = breedId;
    }
}