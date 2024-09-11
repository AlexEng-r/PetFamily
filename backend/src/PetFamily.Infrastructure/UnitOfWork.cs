using System.Data.Common;
using Microsoft.EntityFrameworkCore.Storage;
using PetFamily.Application.Database;
using PetFamily.Infrastructure.DatabaseContext;

namespace PetFamily.Infrastructure;

public class UnitOfWork
    : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        return transaction.GetDbTransaction();
    }

    public async Task SaveChanges(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);
}