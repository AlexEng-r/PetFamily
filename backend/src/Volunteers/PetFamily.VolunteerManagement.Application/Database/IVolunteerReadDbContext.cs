using PetFamily.VolunteerManagement.Application.Dtos;

namespace PetFamily.VolunteerManagement.Application.Database;

public interface IVolunteerReadDbContext
{
    IQueryable<VolunteerDto> Volunteers { get; }

    IQueryable<PetDto> Pets { get; }

    IQueryable<PetPhotoDto> PetPhotos { get; }
}