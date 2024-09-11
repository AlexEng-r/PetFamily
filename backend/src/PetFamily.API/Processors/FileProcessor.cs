using PetFamily.Application.Volunteers.Common;

namespace PetFamily.API.Processors;

public class FileProcessor : IAsyncDisposable
{
    private readonly List<UploadFileDto> _files = [];

    public List<UploadFileDto> Process(IFormFileCollection files)
    {
        foreach (var file in files)
        {
            var stream = file.OpenReadStream();
            var fileContent = new UploadFileDto(stream, file.FileName);
            _files.Add(fileContent);
        }

        return _files;
    }

    public async ValueTask DisposeAsync()
    {
        foreach (var file in _files)
        {
            await file.Stream.DisposeAsync();
        }
    }
}