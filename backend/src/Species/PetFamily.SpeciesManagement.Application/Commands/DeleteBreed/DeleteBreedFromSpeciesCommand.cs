using PetFamily.Core.Abstractions;

namespace PetFamily.SpeciesManagement.Application.Commands.DeleteBreed;

public record DeleteBreedFromSpeciesCommand(Guid SpeciesId, Guid BreedId)
    : ICommand;