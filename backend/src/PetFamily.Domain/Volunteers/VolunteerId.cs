namespace PetFamily.Domain.Volunteers;

public class VolunteerId
{
    public Guid Value { get; }

    private VolunteerId(Guid value)
    {
        Value = value;
    }

    public static VolunteerId NewPetId() => new(Guid.NewGuid());

    public static VolunteerId Empty() => new(Guid.Empty);

    public static VolunteerId Create(Guid id) => new(id);
}