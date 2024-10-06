using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Repositories.Specieses;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagement.Specieses;
using PetFamily.Domain.ValueObjects.String;
using PetFamily.Infrastructure.DatabaseContexts;

namespace PetFamily.Infrastructure.Repositories;

public class SpeciesRepository
    : ISpeciesRepository
{
    private readonly WriteDbContext _dbContext;
    private readonly ILogger<VolunteersRepository> _logger;

    public SpeciesRepository(WriteDbContext dbContext,
        ILogger<VolunteersRepository> logger)
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
}