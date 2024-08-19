using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Repositories.Volunteers;
using PetFamily.Domain.Contacts;
using PetFamily.Domain.Volunteers;
using PetFamily.Infrastructure.DatabaseContext;

namespace PetFamily.Infrastructure.Repositories;

public class VolunteersRepository
    : IVolunteersRepository
{
    private readonly ApplicationDbContext _dbContext;

    public VolunteersRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        await _dbContext.Volunteers.AddAsync(volunteer, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return volunteer.Id.Value;
    }

    public Task<bool> AnyByPhone(ContactPhone phone, CancellationToken cancellationToken = default)
        => _dbContext.Volunteers.AnyAsync(x => x.Phone == phone, cancellationToken);
}