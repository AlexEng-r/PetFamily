using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects.Ids;
using PetFamily.SharedKernel.ValueObjects.String;
using PetFamily.SpeciesManagement.Application.Repositories;
using PetFamily.SpeciesManagement.Domain;
using PetFamily.SpeciesManagement.Infrastructure.DatabaseContexts;

namespace PetFamily.SpeciesManagement.Infrastructure.Repositories;

public class SpeciesRepository
    : ISpeciesRepository
{
    private readonly SpeciesWriteDbContext _dbContext;
    private readonly ILogger<SpeciesRepository> _logger;

    public SpeciesRepository(SpeciesWriteDbContext dbContext,
        ILogger<SpeciesRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Guid> Add(Species species, CancellationToken cancellationToken = default)
    {
        await _dbContext.Species.AddAsync(species, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Species added {name}", species.Name.Value);

        return species.Id.Value;
    }

    public Task<bool> AnyByName(NotEmptyString name, CancellationToken cancellationToken = default)
        => _dbContext.Species.AnyAsync(x => x.Name == name, cancellationToken);

    public async Task<Result<Species, Error>> GetById(SpeciesId speciesId,
        CancellationToken cancellationToken = default)
    {
        var species = await _dbContext.Species
            .Where(x => x.Id == speciesId)
            .Include(x => x.Breeds)
            .FirstOrDefaultAsync(cancellationToken);

        if (species == null)
        {
            return Errors.General.NotFound(speciesId.Value);
        }

        return species;
    }

    public Task Delete(Species species, CancellationToken cancellationToken = default)
    {
        species.Delete();

        foreach (var breed in species.Breeds)
        {
            breed.Delete();
        }

        return Task.CompletedTask;
    }
}