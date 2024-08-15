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

    public PetPhoto(NotEmptyString path, bool isMain)
    {
        Path = path;
        IsMain = isMain;
    }
}