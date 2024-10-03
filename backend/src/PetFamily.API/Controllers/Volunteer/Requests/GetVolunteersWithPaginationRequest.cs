using PetFamily.Application.VolunteerManagement.Queries.GetVolunteersWithPagination;

namespace PetFamily.API.Controllers.Volunteer.Requests;

public record GetVolunteersWithPaginationRequest(
    int? Experience,
    string? SortBy,
    string? SortDirection,
    int Page,
    int PageSize)
{
    public GetFilteredVolunteersWithPaginationQuery ToQuery()
        => new(Experience,
            SortBy,
            SortDirection,
            Page,
            PageSize);
}