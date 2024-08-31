namespace PetFamily.Application.Repositories;

public interface ICommonRepository
{
    Task SaveChanges(CancellationToken cancellationToken = default);
}