using PetFamily.Domain.SeedWork.Entities.BaseDomain;

namespace PetFamily.Domain.PetPhotos;

public class PetPhoto
    : Entity
{
    public string Path { get; private set; }

    public bool IsMain { get; private set; }
}