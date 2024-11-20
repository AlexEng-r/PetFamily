namespace PetFamily.VolunteerManagement.Contracts;

public interface IVolunteerContract
{
    Task<bool> IsSpeciesUsed(Guid speciesId);
    
    Task<bool> IsBreedUsed(Guid breedId);
}