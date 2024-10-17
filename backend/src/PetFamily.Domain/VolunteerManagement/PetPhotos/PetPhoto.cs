using PetFamily.Domain.Shared.Entities.BaseDomain;
using PetFamily.Domain.ValueObjects.String;

namespace PetFamily.Domain.VolunteerManagement.PetPhotos;

public class PetPhoto
    : Entity<PetPhotoId>
{
    public NotEmptyString Path { get; private set; }

    public string HashCode { get; private set; }

    public bool IsMain { get; private set; }

    public string BucketName { get; private set; }

    private PetPhoto(PetPhotoId id)
        : base(id)
    {
    }

    public PetPhoto(PetPhotoId id, NotEmptyString path, string hashCode, bool isMain, string bucketName)
        : base(id)
    {
        Path = path;
        HashCode = hashCode;
        IsMain = isMain;
        BucketName = bucketName;
    }

    public void SetMain(bool main = true) => IsMain = main;
}