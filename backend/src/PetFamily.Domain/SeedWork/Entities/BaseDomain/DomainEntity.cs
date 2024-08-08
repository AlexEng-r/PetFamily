namespace PetFamily.Domain.SeedWork.Entities.BaseDomain;

public abstract class DomainEntity
    : IDomainEntityCreated
{
    public Guid Id { get; } = Guid.NewGuid();

    public DateTime DateCreated { get; }

    public override bool Equals(object? obj) =>
        obj is DomainEntity entity && Id.Equals(entity.Id) && GetType() == entity.GetType();

    public override int GetHashCode() => Id.GetHashCode();
}