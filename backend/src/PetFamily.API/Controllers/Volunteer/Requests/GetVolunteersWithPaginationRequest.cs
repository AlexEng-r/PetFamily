using PetFamily.Application.Dtos;
using PetFamily.Application.VolunteerManagement.Queries.GetVolunteersWithPagination;

namespace PetFamily.API.Controllers.Volunteer.Requests;

public record GetVolunteersWithPaginationRequest(FullNameDto? FullNameDto, int Page, int PageSize)
{
    public GetFilteredVolunteersWithPaginationQuery ToQuery() => new(FullNameDto, Page, PageSize);
}