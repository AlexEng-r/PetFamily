using PetFamily.Application.MessageQueues;
using PetFamily.Application.Providers;
using PetFamily.Application.Providers.File;
using PetFamily.Application.Services;
using FileInfo = PetFamily.Application.Providers.File.FileInfo;

namespace PetFamily.Infrastructure.Services;

public class FileCleanerService
    : IFileCleanerService
{
    private readonly IFileProvider _fileProvider;
    private readonly IMessageQueue<IEnumerable<FileInfo>> _messageQueue;

    public FileCleanerService(IFileProvider fileProvider,
        IMessageQueue<IEnumerable<FileInfo>> messageQueue)
    {
        _fileProvider = fileProvider;
        _messageQueue = messageQueue;
    }

    public async Task Process(CancellationToken cancellationToken)
    {
        var fileInfos = await _messageQueue.ReadAsync(cancellationToken);

        foreach (var fileInfo in fileInfos)
        {
            await _fileProvider.DeleteFileAsync(fileInfo, cancellationToken);
        }
    }
}