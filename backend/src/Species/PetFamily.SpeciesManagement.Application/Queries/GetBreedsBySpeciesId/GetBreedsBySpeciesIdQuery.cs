using PetFamily.Core.Abstractions;

namespace PetFamily.SpeciesManagement.Application.Queries.GetBreedsBySpeciesId;

public record GetBreedsBySpeciesIdQuery(Guid SpeciesId)
    : IQuery;