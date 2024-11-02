using PetFamily.VolunteerManagement.Application.Queries.GetVolunteersWithPagination;

namespace PetFamily.VolunteerManagement.Presentation.Requests;

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