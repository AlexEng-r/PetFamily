using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PetFamily.Application.Services;

namespace PetFamily.Infrastructure.BackgroundServices;

public class FileCleanerBackgroundService
    : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public FileCleanerBackgroundService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var fileCleanerService = scope.ServiceProvider.GetRequiredService<IFileCleanerService>();
        while (!cancellationToken.IsCancellationRequested)
        {
            await fileCleanerService.Process(cancellationToken);
        }

        await Task.CompletedTask;
    }
}