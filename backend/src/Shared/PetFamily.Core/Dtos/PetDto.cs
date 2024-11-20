using PetFamily.Core.Enums;

namespace PetFamily.Core.Dtos;

public class PetDto
{
    public Guid Id { get; set; }

    public Guid VolunteerId { get; set; }

    public string NickName { get; set; }

    public string AnimalType { get; set; }

    public string? Description { get; set; }

    public string Color { get; set; }

    public string? HealthInformation { get; set; }

    public AddressDto Address { get; set; }

    public double? Weight { get; set; }

    public double? Height { get; set; }

    public string Phone { get; set; }

    public bool IsSterialized { get; set; }

    public DateTime? BirthDayDate { get; set; }

    public bool IsVaccinated { get; set; }

    public StatusType Status { get; set; }

    public IReadOnlyList<RequisiteDto> Requisites { get; set; }

    public Guid SpeciesId { get; set; }

    public Guid BreedId { get; set; }

    public int Position { get; set; }

    public DateTime DateCreated { get; set; }

    public bool IsDeleted { get; set; }
}