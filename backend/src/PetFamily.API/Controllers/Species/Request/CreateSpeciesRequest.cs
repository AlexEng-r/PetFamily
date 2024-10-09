using PetFamily.Application.SpeciesManagement.Commands.CreateSpecies;

namespace PetFamily.API.Controllers.Species.Request;

public record CreateSpeciesRequest(string Name)
{
    public CreateSpeciesCommand ToCommand() => new(Name);
}