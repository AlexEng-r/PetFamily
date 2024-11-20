using CSharpFunctionalExtensions;
using PetFamily.Core.Dtos;
using PetFamily.SharedKernel;

namespace PetFamily.SpeciesManagement.Contracts;

public interface ISpeciesContract
{
    Task<Result<SpeciesDto, Error>> GetSpeciesById(Guid speciesId, CancellationToken cancellationToken);
}