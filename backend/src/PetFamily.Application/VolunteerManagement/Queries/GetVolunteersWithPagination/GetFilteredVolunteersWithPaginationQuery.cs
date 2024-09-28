using PetFamily.Application.Abstractions;
using PetFamily.Application.Dtos;

namespace PetFamily.Application.VolunteerManagement.Queries.GetVolunteersWithPagination;

public record GetFilteredVolunteersWithPaginationQuery(FullNameDto? FullName,int Page, int PageSize)
    : IQuery;