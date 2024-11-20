using PetFamily.SpeciesManagement.Application.Commands.DeleteBreed;

namespace PetFamily.SpeciesManagement.Presentation.Request;

public record DeleteBreedFromSpeciesRequest(Guid BreedId)
{
    public DeleteBreedFromSpeciesCommand ToCommand(Guid speciesId) => new(speciesId, BreedId);
}