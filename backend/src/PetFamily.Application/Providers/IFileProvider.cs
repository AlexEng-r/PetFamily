using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Providers;

public interface IFileProvider
{
    Task<Result<string, Error>> GetFileByNameAsync(string fileName, CancellationToken cancellationToken = default);

    Task<Result<string, Error>> DeleteFileAsync(string fileName, CancellationToken cancellationToken = default);

    Task<Result<string, Error>> UploadFileAsync(Stream stream, string objectName,
        CancellationToken cancellationToken = default);
}