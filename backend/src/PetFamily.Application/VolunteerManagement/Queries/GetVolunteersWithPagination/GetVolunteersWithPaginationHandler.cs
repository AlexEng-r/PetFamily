using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Application.Extensions;
using PetFamily.Application.Models;

namespace PetFamily.Application.VolunteerManagement.Queries.GetVolunteersWithPagination;

public class GetVolunteersWithPaginationHandler
    : IQueryHandler<GetFilteredVolunteersWithPaginationQuery, PagedList<VolunteerDto>>
{
    private readonly IReadDbContext _readDbContext;

    public GetVolunteersWithPaginationHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<PagedList<VolunteerDto>> Handle(GetFilteredVolunteersWithPaginationQuery query,
        CancellationToken cancellationToken)
    {
        var volunteerQuery = _readDbContext.Volunteers;
        if (query.FullName != null)
        {
            
        }
        

        return await volunteerQuery.ToPagedList(query.Page, query.PageSize, cancellationToken);
    }
}