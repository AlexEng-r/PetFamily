using PetFamily.Core.MessageQueues;
using PetFamily.Core.Providers.File;
using FileInfo = PetFamily.Core.Providers.File.FileInfo;

namespace PetFamily.Core.Services;

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