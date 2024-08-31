using PetFamily.Application.Repositories;
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
        // Реализация SofrDelete
        
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}