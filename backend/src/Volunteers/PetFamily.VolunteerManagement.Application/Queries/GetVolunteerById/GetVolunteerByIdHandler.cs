using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;
using PetFamily.VolunteerManagement.Application.Database;
using PetFamily.VolunteerManagement.Application.Dtos;

namespace PetFamily.VolunteerManagement.Application.Queries.GetVolunteerById;

public class GetVolunteerByIdHandler
    : IQueryHandler<GetVolunteerByIdQuery, VolunteerDto>
{
    private readonly IVolunteerReadDbContext _volunteerReadDbContext;
    private readonly IValidator<GetVolunteerByIdQuery> _validator;

    public GetVolunteerByIdHandler(IVolunteerReadDbContext volunteerReadDbContext,
        IValidator<GetVolunteerByIdQuery> validator)
    {
        _volunteerReadDbContext = volunteerReadDbContext;
        _validator = validator;
    }

    public async Task<Result<VolunteerDto, ErrorList>> Handle(GetVolunteerByIdQuery query,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }

        var volunteer = await _volunteerReadDbContext.Volunteers
            .FirstOrDefaultAsync(v => v.Id == query.VolunteerId, cancellationToken);
        if (volunteer == null)
        {
            return Errors.General.NotFound(query.VolunteerId).ToErrorList();
        }

        return volunteer;
    }
}