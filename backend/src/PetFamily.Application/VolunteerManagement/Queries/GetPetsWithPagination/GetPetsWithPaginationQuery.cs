using PetFamily.Application.Abstractions;
using PetFamily.Domain.VolunteerManagement.Enums;

namespace PetFamily.Application.VolunteerManagement.Queries.GetPetsWithPagination;

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