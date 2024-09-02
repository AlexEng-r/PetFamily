using CSharpFunctionalExtensions;
using PetFamily.Domain.Contacts;
using PetFamily.Domain.SeedWork;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Repositories.Volunteers;

public interface IVolunteersRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);

    Task<bool> AnyByPhone(ContactPhone phone, CancellationToken cancellationToken = default);

    Task<Result<Volunteer, Error>> GetById(VolunteerId volunteerId, CancellationToken cancellationToken = default);
}