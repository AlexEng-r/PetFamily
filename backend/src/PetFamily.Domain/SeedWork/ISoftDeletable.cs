namespace PetFamily.Domain.SeedWork;

public interface ISoftDeletable
{
    void Delete();
    void Restore();
}