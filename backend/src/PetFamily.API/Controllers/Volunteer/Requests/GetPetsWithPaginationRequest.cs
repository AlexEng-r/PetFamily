using PetFamily.Application.VolunteerManagement.Queries.GetPetsWithPagination;
using PetFamily.Domain.VolunteerManagement.Enums;

namespace PetFamily.API.Controllers.Volunteer.Requests;

public record GetPetsWithPaginationRequest(
    Guid? VolunteerId,
    string? NickName,
    Guid? Species,
    Guid? Breed,
    bool? IsSterialized,
    bool? IsVaccinated,
    StatusType? Status,
    string? SortBy,
    string? SortDirection,
    int Page,
    int PageSize)
{
    public GetPetsWithPaginationQuery ToCommand()
        => new(
            VolunteerId,
            NickName,
            Species,
            Breed,
            IsSterialized,
            IsVaccinated,
            Status,
            SortBy,
            SortDirection,
            Page,
            PageSize);
}