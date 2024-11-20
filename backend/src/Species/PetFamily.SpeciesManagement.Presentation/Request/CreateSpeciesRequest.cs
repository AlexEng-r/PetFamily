using PetFamily.SpeciesManagement.Application.Commands.CreateSpecies;

namespace PetFamily.SpeciesManagement.Presentation.Request;

public record CreateSpeciesRequest(string Name)
{
    public CreateSpeciesCommand ToCommand() => new(Name);
}