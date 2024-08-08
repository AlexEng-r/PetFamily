using PetFamily.Domain.SeedWork.Entities;

namespace PetFamily.Domain.Aggregates.Pets;

public class PetOption
    : IValueObject
{
    public string? Description { get; }

    public string? Breed { get; }

    public string? HealthInformation { get; }

    public double? Weight { get; }

    public double? Height { get; }

    public DateTime? BirthDayDate { get; }

    private PetOption()
    {
    }

    public PetOption(string? description, string? breed, string? healthInformation, double? weight, double? height,
        DateTime? birthDayDate)
    {
        Description = description;
        Breed = breed;
        HealthInformation = healthInformation;
        Weight = weight;
        Height = height;
        BirthDayDate = birthDayDate;
    }
}