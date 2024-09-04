namespace PetFamily.Infrastructure.Options;

public class MinioOptions
{
    public const string SECTION_NAME = "Minio";

    public string Endpoint { get; init; }
    public string AccessKey { get; init; }
    public string SecretKey { get; init; }
    public bool EnableSSl { get; init; }
}