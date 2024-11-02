using PetFamily.SharedKernel.ValueObjects.Ids;

namespace PetFamily.VolunteerManagement.Domain.Entities.Pets.SpeciesDetails;

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