using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Repositories;
using PetFamily.Domain.SeedWork;
using PetFamily.Infrastructure.DatabaseContext;

namespace PetFamily.Infrastructure.Repositories;

public class CommonRepository
    : ICommonRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CommonRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveChanges(CancellationToken cancellationToken = default)
    {
        var deletedEntries = _dbContext.ChangeTracker
            .Entries<ISoftDeletable>()
            .Where(e => e.State == EntityState.Deleted);

        foreach (var entry in deletedEntries)
        {
            entry.State = EntityState.Modified;
            entry.Entity.Delete();
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}