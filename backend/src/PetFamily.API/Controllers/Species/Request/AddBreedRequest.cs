using PetFamily.Application.SpeciesManagement.Commands.AddBreed;

namespace PetFamily.API.Controllers.Species.Request;

public record AddBreedRequest(string BreedName)
{
    public AddBreedCommand ToCommand(Guid speciesId) => new(speciesId, BreedName);
}