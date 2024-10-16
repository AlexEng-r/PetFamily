namespace PetFamily.Application.Providers.File;

public record FileData(Stream Stream, string HashCode, FileInfo FileInfo);