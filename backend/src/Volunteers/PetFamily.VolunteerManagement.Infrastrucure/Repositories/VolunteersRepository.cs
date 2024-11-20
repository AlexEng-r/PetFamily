using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects.Contacts;
using PetFamily.SharedKernel.ValueObjects.Ids;
using PetFamily.VolunteerManagement.Application.Repositories;
using PetFamily.VolunteerManagement.Domain;
using PetFamily.VolunteerManagement.Domain.Entities.PetPhotos;
using PetFamily.VolunteerManagement.Infrastrucure.DatabaseContexts;

namespace PetFamily.VolunteerManagement.Infrastrucure.Repositories;

public class VolunteersRepository
    : IVolunteersRepository
{
    private readonly VolunteerWriteDbContext _dbContext;
    private readonly ILogger<VolunteersRepository> _logger;

    public VolunteersRepository(VolunteerWriteDbContext dbContext,
        ILogger<VolunteersRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        await _dbContext.Volunteers.AddAsync(volunteer, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Volunteer added {fullname}", volunteer.FullName.ToString());

        return volunteer.Id.Value;
    }

    public async Task<bool> AnyByPhone(ContactPhone phone, CancellationToken cancellationToken = default)
        => await _dbContext.Volunteers.AnyAsync(x => x.Phone == phone, cancellationToken);

    public async Task<Result<Volunteer, Error>> GetById(VolunteerId volunteerId,
        CancellationToken cancellationToken = default)
    {
        var volunteer = await _dbContext.Volunteers
            .Where(x => x.Id == volunteerId)
            .Include(x => x.Pets)
            .ThenInclude(x => x.PetPhotos)
            .FirstOrDefaultAsync(cancellationToken);

        if (volunteer == null)
        {
            return Errors.General.NotFound(volunteerId.Value);
        }

        return volunteer;
    }

    public Task Delete(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        volunteer.Delete();

        foreach (var pet in volunteer.Pets)
        {
            pet.Delete();
        }

        return Task.CompletedTask;
    }

    public Task DeletePhotoFromPet(PetPhoto petPhoto, CancellationToken cancellationToken = default)
        => Task.FromResult(_dbContext.Remove(petPhoto));
    
}