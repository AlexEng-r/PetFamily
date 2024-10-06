using PetFamily.Application.Abstractions;

namespace PetFamily.Application.SpeciesManagement.Commands.AddBreed;

public record AddBreedCommand(Guid SpeciesId, string BreedName)
    : ICommand;