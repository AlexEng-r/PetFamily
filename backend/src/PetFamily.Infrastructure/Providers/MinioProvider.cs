using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using PetFamily.Application.Providers;
using PetFamily.Domain.Shared;

namespace PetFamily.Infrastructure.Providers;

public class MinioProvider
    : IFileProvider
{
    private readonly IMinioClient _minioClient;
    private readonly ILogger<MinioProvider> _logger;

    private const string BUCKET_NAME = "photos";
    private const int EXPIRE_PERIOD = 60 * 60 * 24;

    public MinioProvider(
        IMinioClient minioClient,
        ILogger<MinioProvider> logger)
    {
        _minioClient = minioClient;
        _logger = logger;
    }

    public async Task<Result<string, Error>> GetFileByNameAsync(string fileName,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var args = new PresignedGetObjectArgs()
                .WithBucket(BUCKET_NAME)
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

    public async Task<Result<string, Error>> DeleteFileAsync(string fileName,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var removeArgs = new RemoveObjectArgs()
                .WithBucket(BUCKET_NAME)
                .WithObject(fileName);

            await _minioClient.RemoveObjectAsync(removeArgs, cancellationToken);

            return fileName;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "fail to delete file from minio");
            return Error.Failure("file.delete", "fail to delete file from minio");
        }
    }

    public async Task<Result<string, Error>> UploadFileAsync(Stream stream, string objectName,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var bucketExistArgs = new BucketExistsArgs()
                .WithBucket(BUCKET_NAME);

            var bucketExistResult = await _minioClient.BucketExistsAsync(bucketExistArgs, cancellationToken);

            if (!bucketExistResult)
            {
                var bucketMakeArgs = new MakeBucketArgs()
                    .WithBucket(BUCKET_NAME);

                await _minioClient.MakeBucketAsync(bucketMakeArgs, cancellationToken);
            }

            var putObjectArgs = new PutObjectArgs()
                .WithBucket(BUCKET_NAME)
                .WithStreamData(stream)
                .WithObjectSize(stream.Length)
                .WithObject(objectName);

            var result = await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);

            return result.ObjectName;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "fail to upload file to minio");
            return Error.Failure("file.upload", "fail to upload file to minio");
        }
    }
}