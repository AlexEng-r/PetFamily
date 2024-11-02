using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects.Ids;
using PetFamily.SharedKernel.ValueObjects.String;
using PetFamily.SpeciesManagement.Domain;

namespace PetFamily.SpeciesManagement.Application.Repositories;

public interface ISpeciesRepository
{
    Task<Guid> Add(Species species, CancellationToken cancellationToken = default);

    Task<bool> AnyByName(NotEmptyString name, CancellationToken cancellationToken = default);

    Task<Result<Species, Error>> GetById(SpeciesId speciesId,
        CancellationToken cancellationToken = default);

    Task Delete(Species species, CancellationToken cancellationToken = default);
}