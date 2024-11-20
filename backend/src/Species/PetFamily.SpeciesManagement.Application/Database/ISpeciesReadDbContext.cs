using PetFamily.Core.Dtos;

namespace PetFamily.SpeciesManagement.Application.Database;

public interface ISpeciesReadDbContext
{
    IQueryable<SpeciesDto> Species { get; }

    IQueryable<BreedDto> Breeds { get; }
}