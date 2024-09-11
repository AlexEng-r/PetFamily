using System.Data.Common;

namespace PetFamily.Application.Database;

public interface IUnitOfWork
{
    public Task<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

    public Task SaveChanges(CancellationToken cancellationToken = default);
}