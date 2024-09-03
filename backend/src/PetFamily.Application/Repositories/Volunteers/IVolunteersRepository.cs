using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects.Contacts;
using PetFamily.Domain.VolunteerManagement.Volunteers;

namespace PetFamily.Application.Repositories.Volunteers;

public interface IVolunteersRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);

    Task<bool> AnyByPhone(ContactPhone phone, CancellationToken cancellationToken = default);

    Task<Result<Volunteer, Error>> GetById(VolunteerId volunteerId, CancellationToken cancellationToken = default);

    Task Delete(Volunteer volunteer, CancellationToken cancellationToken = default);
}