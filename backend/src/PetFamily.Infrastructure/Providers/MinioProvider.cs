using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using PetFamily.Application.Providers;
using PetFamily.Domain.Shared;
using FileInfo = PetFamily.Application.Providers.FileInfo;

namespace PetFamily.Infrastructure.Providers;

public class MinioProvider
    : IFileProvider
{
    private readonly IMinioClient _minioClient;
    private readonly ILogger<MinioProvider> _logger;

    private const int EXPIRE_PERIOD = 60 * 60 * 24;
    private const int MAX_LIMIT_THREADS = 3;

    public MinioProvider(
        IMinioClient minioClient,
        ILogger<MinioProvider> logger)
    {
        _minioClient = minioClient;
        _logger = logger;
    }

    public async Task<Result<string, Error>> GetFileByNameAsync(string fileName, string bucketName,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var args = new PresignedGetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(fileName)
                .WithExpiry(EXPIRE_PERIOD);

            var result = await _minioClient.PresignedGetObjectAsync(args);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "fail to get file from minio");
            return Error.Failure("file.get", "fail to get file from minio");
        }
    }

    public async Task<UnitResult<Error>> DeleteFileAsync(FileInfo fileInfo,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var statArgs = new StatObjectArgs()
                .WithBucket(fileInfo.BucketName)
                .WithObject(fileInfo.FilePath);

            var statResult = await _minioClient.StatObjectAsync(statArgs, cancellationToken);
            if (statResult != null)
            {
                var removeArgs = new RemoveObjectArgs()
                    .WithBucket(fileInfo.BucketName)
                    .WithObject(fileInfo.FilePath);

                await _minioClient.RemoveObjectAsync(removeArgs, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "fail to delete file from minio");
            return Error.Failure("file.delete", "fail to delete file from minio");
        }

        return Result.Success<Error>();
    }

    public async Task<Result<IReadOnlyList<string>, Error>> UploadFilesAsync(IEnumerable<FileData> files,
        CancellationToken cancellationToken = default)
    {
        var semaphoreSlim = new SemaphoreSlim(MAX_LIMIT_THREADS);

        var fileContents = files.ToList();
        try
        {
            await IfBucketsNotExistCreateBucket(fileContents, cancellationToken);

            var tasks = fileContents
                .Select(async f => await PutObject(f, semaphoreSlim, cancellationToken));

            var pathsResult = await Task.WhenAll(tasks);

            if (pathsResult.Any(p => p.IsFailure))
                return pathsResult.First().Error;

            var results = pathsResult.Select(p => p.Value).ToList();

            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Fail to upload files in minio, files amount: {amount}", fileContents.Count);

            return Error.Failure("file.upload", "Fail to upload files in minio");
        }
    }

    private async Task IfBucketsNotExistCreateBucket(
        IEnumerable<FileData> fileContents,
        CancellationToken cancellationToken)
    {
        HashSet<string> bucketNames = [..fileContents.Select(file => file.FileInfo.BucketName)];

        foreach (var bucketName in bucketNames)
        {
            var bucketExistArgs = new BucketExistsArgs()
                .WithBucket(bucketName);

            var bucketExist = await _minioClient
                .BucketExistsAsync(bucketExistArgs, cancellationToken);

            if (bucketExist == false)
            {
                var makeBucketArgs = new MakeBucketArgs()
                    .WithBucket(bucketName);

                await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
            }
        }
    }

    private async Task<Result<string, Error>> PutObject(
        FileData fileData,
        SemaphoreSlim semaphoreSlim,
        CancellationToken cancellationToken)
    {
        await semaphoreSlim.WaitAsync(cancellationToken);

        var putObjectArgs = new PutObjectArgs()
            .WithBucket(fileData.FileInfo.BucketName)
            .WithStreamData(fileData.Stream)
            .WithObjectSize(fileData.Stream.Length)
            .WithObject(fileData.FileInfo.FilePath);

        try
        {
            var response = await _minioClient
                .PutObjectAsync(putObjectArgs, cancellationToken);

            return response.ObjectName;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Fail to upload file in minio with path {objectName} in bucket {bucket}",
                fileData.FileInfo.FilePath,
                fileData.FileInfo.BucketName);

            return Error.Failure("file.upload", "Fail to upload file in minio");
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }
}