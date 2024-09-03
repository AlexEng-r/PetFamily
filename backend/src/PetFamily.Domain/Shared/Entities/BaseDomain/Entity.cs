namespace PetFamily.Domain.Shared.Entities.BaseDomain;

public abstract class Entity<TId>
    where TId: notnull
{
    public TId Id { get; }

    protected Entity()
    {
    }

    protected Entity(TId id)
    {
        Id = id;
    }

    public static bool operator ==(Entity<TId>? first, Entity<TId>? second)
    {
        if (ReferenceEquals(first, null) && ReferenceEquals(second, null))
            return true;

        if (ReferenceEquals(first, null) || ReferenceEquals(second, null))
            return false;

        return first.Equals(second);
    }

    public static bool operator !=(Entity<TId>? first, Entity<TId>? second)
    {
        return !(first == second);
    }

    public override bool Equals(object? obj) => obj is Entity<TId> other &&
                                                Id.Equals(other.Id) &&
                                                GetType() == obj.GetType();

    public override int GetHashCode()
    {
        return (GetType().ToString() + Id).GetHashCode();
    }
}