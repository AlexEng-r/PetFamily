namespace PetFamily.Core.Services;

public interface IFileCleanerService
{
    Task Process(CancellationToken cancellationToken);
}