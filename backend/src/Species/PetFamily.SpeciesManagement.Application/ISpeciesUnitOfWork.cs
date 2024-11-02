using System.Data.Common;

namespace PetFamily.SpeciesManagement.Application;

public interface ISpeciesUnitOfWork
{
    public Task<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

    public Task SaveChanges(CancellationToken cancellationToken = default);
}