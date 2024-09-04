using CSharpFunctionalExtensions;
using PetFamily.Application.Providers;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.MinioTest;

public class MinioTestHandler
{
    private readonly IFileProvider _fileProvider;

    public MinioTestHandler(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }

    public Task<Result<string, Error>> GetFileAsync(string fileName, CancellationToken cancellationToken)
        => _fileProvider.GetFileByNameAsync(fileName, cancellationToken);

    public Task<Result<string, Error>> UploadFileAsync(Stream fileStream, string objectName,
        CancellationToken cancellationToken)
        => _fileProvider.UploadFileAsync(fileStream, objectName, cancellationToken);

    public Task<Result<string, Error>> DeleteFileAsync(string fileName, CancellationToken cancellationToken)
        => _fileProvider.DeleteFileAsync(fileName, cancellationToken);
}