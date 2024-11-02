using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects.Contacts;
using PetFamily.SharedKernel.ValueObjects.Ids;
using PetFamily.VolunteerManagement.Domain;
using PetFamily.VolunteerManagement.Domain.Entities.PetPhotos;

namespace PetFamily.VolunteerManagement.Application.Repositories;

public interface IVolunteersRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);

    Task<bool> AnyByPhone(ContactPhone phone, CancellationToken cancellationToken = default);

    Task<Result<Volunteer, Error>> GetById(VolunteerId volunteerId, CancellationToken cancellationToken = default);

    Task Delete(Volunteer volunteer, CancellationToken cancellationToken = default);

    Task DeletePhotoFromPet(PetPhoto petPhoto, CancellationToken cancellationToken = default);
}