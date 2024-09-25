using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PetFamily.Application.MessageQueues;
using PetFamily.Application.Services;
using FileInfo = PetFamily.Application.Providers.FileInfo;

namespace PetFamily.Infrastructure.BackgroundServices;

public class FileCleanerBackgroundService
    : BackgroundService
{
    private readonly IMessageQueue<IEnumerable<FileInfo>> _messageQueue;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public FileCleanerBackgroundService(IMessageQueue<IEnumerable<FileInfo>> messageQueue,
        IServiceScopeFactory serviceScopeFactory)
    {
        _messageQueue = messageQueue;
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