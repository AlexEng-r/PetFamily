using System.Data.Common;
using Microsoft.EntityFrameworkCore.Storage;
using PetFamily.VolunteerManagement.Application;
using PetFamily.VolunteerManagement.Infrastrucure.DatabaseContexts;

namespace PetFamily.VolunteerManagement.Infrastrucure;

public class VolunteerUnitOfWork
    : IVolunteerUnitOfWork
{
    private readonly VolunteerWriteDbContext _context;

    public VolunteerUnitOfWork(VolunteerWriteDbContext context)
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