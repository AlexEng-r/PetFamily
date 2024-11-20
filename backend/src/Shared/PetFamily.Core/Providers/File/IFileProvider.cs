using CSharpFunctionalExtensions;
using PetFamily.Core.Dtos;
using PetFamily.SharedKernel;

namespace PetFamily.Core.Providers.File;

public interface IFileProvider
{
    Task<Result<string, Error>> GetFileByNameAsync(string fileName, string bucketName,
        CancellationToken cancellationToken = default);

    Task<UnitResult<Error>> DeleteFileAsync(FileInfo fileInfo,
        CancellationToken cancellationToken = default);

    Task<Result<IReadOnlyList<UploadFileProviderOutputDto>, Error>> UploadFilesAsync(IEnumerable<FileData> files,
        CancellationToken cancellationToken = default);
}