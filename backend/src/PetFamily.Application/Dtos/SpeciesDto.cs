namespace PetFamily.Application.Dtos;

public class SpeciesDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsDeleted { get; set; }
}