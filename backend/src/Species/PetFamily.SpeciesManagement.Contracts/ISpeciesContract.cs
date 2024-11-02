using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;
using PetFamily.SpeciesManagement.Domain;

namespace PetFamily.SpeciesManagement.Contracts;

public interface ISpeciesContract
{
    Task<Result<Species, Error>> GetSpeciesById(Guid speciesId, CancellationToken cancellationToken);
}