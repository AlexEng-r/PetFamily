using System.Data.Common;

namespace PetFamily.VolunteerManagement.Application;

public interface IVolunteerUnitOfWork
{
    public Task<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

    public Task SaveChanges(CancellationToken cancellationToken = default);
}