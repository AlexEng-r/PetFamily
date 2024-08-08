using PetFamily.Domain.SeedWork.Entities;

namespace PetFamily.Domain.Aggregates.Pets;

public class PetOption
    : IValueObject
{
    public string? Description { get; private set; }

    public string? Breed { get; private set; }

    public string? HealthInformation { get; private set; }

    public double? Weight { get; private set; }

    public double? Height { get; private set; }

    public DateTime? BirthDayDate { get; private set; }

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