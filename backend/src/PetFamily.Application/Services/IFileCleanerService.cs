namespace PetFamily.Application.Services;

public interface IFileCleanerService
{
    Task Process(CancellationToken cancellationToken);
}