using PetFamily.SharedKernel.Entities.BaseDomain;
using PetFamily.SharedKernel.ValueObjects.Ids;
using PetFamily.SharedKernel.ValueObjects.String;

namespace PetFamily.VolunteerManagement.Domain.Entities.PetPhotos;

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