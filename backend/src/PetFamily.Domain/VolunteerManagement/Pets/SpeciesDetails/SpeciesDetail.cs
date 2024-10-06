using PetFamily.Domain.SpeciesManagement.Specieses;

namespace PetFamily.Domain.VolunteerManagement.Pets.SpeciesDetails;

public record SpeciesDetail
{
    public SpeciesId SpeciesId { get; }

    public Guid BreedId { get; }

    public SpeciesDetail(SpeciesId speciesId, Guid breedId)
    {
        SpeciesId = speciesId;
        BreedId = breedId;
    }
}