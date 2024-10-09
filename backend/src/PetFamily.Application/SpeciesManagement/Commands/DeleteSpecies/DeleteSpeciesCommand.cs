using PetFamily.Application.Abstractions;

namespace PetFamily.Application.SpeciesManagement.Commands.DeleteSpecies;

public record DeleteSpeciesCommand(Guid SpeciesId)
    : ICommand;