using PetFamily.Application.Abstractions;

namespace PetFamily.Application.VolunteerManagement.Queries.GetVolunteersWithPagination;

public record GetFilteredVolunteersWithPaginationQuery(
    int? Experience,
    string? SortBy,
    string? SortDirection,
    int Page,
    int PageSize)
    : IQuery;