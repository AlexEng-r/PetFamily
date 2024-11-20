using PetFamily.VolunteerManagement.Application.Database;
using PetFamily.VolunteerManagement.Contracts;

namespace PetFamily.VolunteerManagement.Presentation;

public class VolunteerContract
    : IVolunteerContract
{
    private readonly IVolunteerReadDbContext _readDbContext;

    public VolunteerContract(IVolunteerReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public Task<bool> IsSpeciesUsed(Guid speciesId)
        => Task.FromResult(_readDbContext.Pets.Any(s => s.SpeciesId == speciesId));

    public Task<bool> IsBreedUsed(Guid breedId)
        => Task.FromResult(_readDbContext.Pets.Any(s => s.BreedId == breedId));
}