using PetFamily.Core.Abstractions;

namespace PetFamily.VolunteerManagement.Application.Queries.GetVolunteersWithPagination;

public record GetFilteredVolunteersWithPaginationQuery(
    int? Experience,
    string? SortBy,
    string? SortDirection,
    int Page,
    int PageSize)
    : IQuery;