namespace PetFamily.Domain.Specieses;

public class SpeciesId
{
    public Guid Value { get; }

    private SpeciesId(Guid value)
    {
        Value = value;
    }

    public static SpeciesId NewPetId() => new(Guid.NewGuid());

    public static SpeciesId Empty() => new(Guid.Empty);

    public static SpeciesId Create(Guid id) => new(id);
}