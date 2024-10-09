using PetFamily.Application.Abstractions;

namespace PetFamily.Application.SpeciesManagement.Commands.CreateSpecies;

public record CreateSpeciesCommand(string Name)
    : ICommand;