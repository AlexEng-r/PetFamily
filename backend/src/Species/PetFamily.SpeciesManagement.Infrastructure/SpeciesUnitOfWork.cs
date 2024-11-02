using System.Data.Common;
using Microsoft.EntityFrameworkCore.Storage;
using PetFamily.SpeciesManagement.Application;
using PetFamily.SpeciesManagement.Infrastructure.DatabaseContexts;

namespace PetFamily.SpeciesManagement.Infrastructure;

public class SpeciesUnitOfWork
    : ISpeciesUnitOfWork
{
    private readonly SpeciesWriteDbContext _context;

    public SpeciesUnitOfWork(SpeciesWriteDbContext context)
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