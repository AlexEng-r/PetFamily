using PetFamily.Domain.Specieses;

namespace PetFamily.Domain.SpeciesDetails;

public record SpeciesDetail
{
    public SpeciesId SpeciesId { get; }

    public Guid BreedId { get; }

    private SpeciesDetail(SpeciesId speciesId, Guid breedId)
    {
        SpeciesId = speciesId;
        BreedId = breedId;
    }
}