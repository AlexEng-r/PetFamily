using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Extensions;
using PetFamily.Application.MinioTest;

namespace PetFamily.API.Controllers;

public class FileController
    : BaseController
{
    private readonly MinioTestHandler _minioTestHandler;

    public FileController(MinioTestHandler minioTestHandler)
    {
        _minioTestHandler = minioTestHandler;
    }

    [HttpGet("{fileName}")]
    public async Task<ActionResult> GetFile(string fileName, CancellationToken cancellationToken)
    {
        var result = await _minioTestHandler.GetFileAsync(fileName, cancellationToken);

        return result.IsFailure
            ? result.Error.ToResponse()
            : Ok(result.Value);
    }

    [HttpPost]
    public async Task<ActionResult> UploadFile(IFormFile file, CancellationToken cancellationToken)
    {
        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        
        await using var fileStream = file.OpenReadStream();

        var result = await _minioTestHandler.UploadFileAsync(fileStream, fileName, cancellationToken);

        return result.IsFailure
            ? result.Error.ToResponse()
            : Ok(result.Value);
    }

    [HttpDelete("{fileName}")]
    public async Task<ActionResult> DeleteFile(string fileName, CancellationToken cancellationToken)
    {
        var result = await _minioTestHandler.DeleteFileAsync(fileName, cancellationToken);

        return result.IsFailure
            ? result.Error.ToResponse()
            : Ok(result.Value);
    }
}