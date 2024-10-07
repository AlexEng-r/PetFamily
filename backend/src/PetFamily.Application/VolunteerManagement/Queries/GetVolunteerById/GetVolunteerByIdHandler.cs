using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.VolunteerManagement.Queries.GetVolunteerById;

public class GetVolunteerByIdHandler
    : IQueryHandler<GetVolunteerByIdQuery, VolunteerDto>
{
    private readonly IReadDbContext _readDbContext;

    public GetVolunteerByIdHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<VolunteerDto, ErrorList>> Handle(GetVolunteerByIdQuery command,
        CancellationToken cancellationToken)
    {
        var volunteer = await _readDbContext.Volunteers
            .FirstOrDefaultAsync(v => v.Id == command.VolunteerId, cancellationToken);
        if (volunteer == null)
        {
            return Errors.General.NotFound(command.VolunteerId).ToErrorList();
        }

        return volunteer;
    }
}