using PetFamily.SpeciesManagement.Application.Commands.AddBreed;

namespace PetFamily.SpeciesManagement.Presentation.Request;

public record AddBreedRequest(string BreedName)
{
    public AddBreedCommand ToCommand(Guid speciesId) => new(speciesId, BreedName);
}