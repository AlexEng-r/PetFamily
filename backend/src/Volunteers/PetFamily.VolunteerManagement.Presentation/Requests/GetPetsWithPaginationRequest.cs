using PetFamily.VolunteerManagement.Application.Queries.GetPetsWithPagination;
using PetFamily.VolunteerManagement.Domain.Enums;

namespace PetFamily.VolunteerManagement.Presentation.Requests;

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