using PetFamily.Domain.PetPhotos;
using PetFamily.Domain.Requisites;
using PetFamily.Domain.SeedWork.Entities.BaseDomain;

namespace PetFamily.Domain.Pets;

public class Pet
    : Entity
{
    public string NickName { get; private set; }

    public string AnimalType { get; private set; }

    public string? Description { get; private set; }

    public string? Breed { get; private set; }

    public string Color { get; private set; }

    public string? HealthInformation { get; private set; }

    public string Address { get; private set; }

    public double? Weight { get; private set; }

    public double? Height { get; private set; }

    public string Phone { get; private set; }

    public bool IsSterialized { get; private set; }

    public DateTime? BirthDayDate { get; private set; }

    public bool IsVaccinated { get; private set; }

    public StatusType Status { get; private set; }

    public RequisiteDetails Requisites { get; private set; }

    public DateTime DateCreated { get; private set; }

    private List<PetPhoto> _petPhotos = [];

    public IReadOnlyList<PetPhoto> PetPhotos => _petPhotos.AsReadOnly();

    private Pet()
    {
    }
}