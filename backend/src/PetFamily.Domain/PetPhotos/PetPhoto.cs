using PetFamily.Domain.SeedWork.Entities.BaseDomain;
using PetFamily.Domain.String;

namespace PetFamily.Domain.PetPhotos;

public class PetPhoto
    : Entity<PetPhotoId>
{
    public NotEmptyString Path { get; private set; }

    public bool IsMain { get; private set; }

    private PetPhoto(PetPhotoId id)
        : base(id)
    {
    }

    public PetPhoto(PetPhotoId id, NotEmptyString path, bool isMain)
        : base(id)
    {
        Path = path;
        IsMain = isMain;
    }
}