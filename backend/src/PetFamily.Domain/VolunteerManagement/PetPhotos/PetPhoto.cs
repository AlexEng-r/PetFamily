using PetFamily.Domain.Shared.Entities.BaseDomain;
using PetFamily.Domain.ValueObjects.String;

namespace PetFamily.Domain.VolunteerManagement.PetPhotos;

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