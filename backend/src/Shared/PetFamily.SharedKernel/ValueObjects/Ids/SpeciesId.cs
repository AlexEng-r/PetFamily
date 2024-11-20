namespace PetFamily.SharedKernel.ValueObjects.Ids;

public record SpeciesId
{
    public Guid Value { get; }

    private SpeciesId(Guid value)
    {
        Value = value;
    }

    public static SpeciesId NewSpeciesId() => new(Guid.NewGuid());

    public static SpeciesId Empty() => new(Guid.Empty);

    public static SpeciesId Create(Guid id) => new(id);
    
    public static implicit operator Guid(SpeciesId speciesId) => speciesId.Value;
    
    public static implicit operator SpeciesId(Guid speciesId) => Create(speciesId);
}