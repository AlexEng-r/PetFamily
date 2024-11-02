namespace PetFamily.VolunteerManagement.Application.Dtos;

public class PetPhotoDto
{
    public Guid Id { get; set; }

    public string Path { get; set; }

    public string HashCode { get; set; }

    public string Bucket { get; set; }

    public bool IsMain { get; set; }

    public PetPhotoDto(Guid id, string path, string hashCode, string bucket, bool isMain)
    {
        Id = id;
        Path = path;
        HashCode = hashCode;
        Bucket = bucket;
        IsMain = isMain;
    }
}