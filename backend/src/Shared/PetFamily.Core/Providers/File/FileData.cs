namespace PetFamily.Core.Providers.File;

public record FileData(Stream Stream, string HashCode, FileInfo FileInfo);