using PetFamily.Application.Abstractions;

namespace PetFamily.Application.SpeciesManagement.Commands.DeleteBreed;

public record DeleteBreedFromSpeciesCommand(Guid SpeciesId, Guid BreedId)
    : ICommand;