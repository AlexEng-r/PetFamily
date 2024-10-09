using PetFamily.Application.SpeciesManagement.Commands.DeleteBreed;

namespace PetFamily.API.Controllers.Species.Request;

public record DeleteBreedFromSpeciesRequest(Guid BreedId)
{
    public DeleteBreedFromSpeciesCommand ToCommand(Guid speciesId) => new(speciesId, BreedId);
}