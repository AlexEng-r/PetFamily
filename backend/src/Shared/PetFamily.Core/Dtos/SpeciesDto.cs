namespace PetFamily.Core.Dtos;

public class SpeciesDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public IEnumerable<BreedDto> Breeds { get; set; }
}