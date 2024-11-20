using PetFamily.Core.Abstractions;
using PetFamily.Core.Enums;

namespace PetFamily.VolunteerManagement.Application.Queries.GetPetsWithPagination;

public record GetPetsWithPaginationQuery(
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
    : IQuery;