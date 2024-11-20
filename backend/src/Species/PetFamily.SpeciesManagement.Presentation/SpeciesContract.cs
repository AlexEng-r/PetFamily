using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Core.Dtos;
using PetFamily.SharedKernel;
using PetFamily.SpeciesManagement.Application.Database;
using PetFamily.SpeciesManagement.Contracts;

namespace PetFamily.SpeciesManagement.Presentation;

public class SpeciesContract
    : ISpeciesContract
{
    private readonly ISpeciesReadDbContext _readDbContext;

    public SpeciesContract(ISpeciesReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<SpeciesDto, Error>> GetSpeciesById(Guid speciesId, CancellationToken cancellationToken)
    {
        var speciesDto = await _readDbContext.Species
            .Include(x => x.Breeds)
            .FirstOrDefaultAsync(x => x.Id == speciesId, cancellationToken);

        return speciesDto != null ? speciesDto : Errors.General.NotFound(speciesId);
    }
}