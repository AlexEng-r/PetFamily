using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagement.Specieses;
using PetFamily.Domain.ValueObjects.String;

namespace PetFamily.Application.Repositories.Specieses;

public interface ISpeciesRepository
{
    Task<Guid> Add(Species species, CancellationToken cancellationToken = default);

    Task<bool> AnyByName(NotEmptyString name, CancellationToken cancellationToken = default);

    Task<Result<Species, Error>> GetById(SpeciesId speciesId,
        CancellationToken cancellationToken = default);

    Task Delete(Species species, CancellationToken cancellationToken = default);
}