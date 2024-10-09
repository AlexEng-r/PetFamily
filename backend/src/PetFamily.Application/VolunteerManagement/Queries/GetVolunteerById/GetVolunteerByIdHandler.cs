using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Application.Extensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.VolunteerManagement.Queries.GetVolunteerById;

public class GetVolunteerByIdHandler
    : IQueryHandler<GetVolunteerByIdQuery, VolunteerDto>
{
    private readonly IReadDbContext _readDbContext;
    private readonly IValidator<GetVolunteerByIdQuery> _validator;

    public GetVolunteerByIdHandler(IReadDbContext readDbContext,
        IValidator<GetVolunteerByIdQuery> validator)
    {
        _readDbContext = readDbContext;
        _validator = validator;
    }

    public async Task<Result<VolunteerDto, ErrorList>> Handle(GetVolunteerByIdQuery command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }

        var volunteer = await _readDbContext.Volunteers
            .FirstOrDefaultAsync(v => v.Id == command.VolunteerId, cancellationToken);
        if (volunteer == null)
        {
            return Errors.General.NotFound(command.VolunteerId).ToErrorList();
        }

        return volunteer;
    }
}