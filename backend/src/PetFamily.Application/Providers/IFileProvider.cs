using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Providers;

public interface IFileProvider
{
    Task<Result<string, Error>> GetFileByNameAsync(string fileName, string bucketName,
        CancellationToken cancellationToken = default);

    Task<UnitResult<Error>> DeleteFileAsync(FileInfo fileInfo,
        CancellationToken cancellationToken = default);

    Task<Result<IReadOnlyList<string>, Error>> UploadFilesAsync(IEnumerable<FileData> files,
        CancellationToken cancellationToken = default);
}