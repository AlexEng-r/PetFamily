using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;
using PetFamily.VolunteerManagement.Application.Database;
using PetFamily.VolunteerManagement.Application.Dtos;

namespace PetFamily.VolunteerManagement.Application.Queries.GetPetById;

public class GetPetByIdHandler
    : IQueryHandler<GetPetByIdQuery, PetDto>
{
    private readonly IVolunteerReadDbContext _volunteerReadDbContext;
    private readonly IValidator<GetPetByIdQuery> _validator;

    public GetPetByIdHandler(IVolunteerReadDbContext volunteerReadDbContext,
        IValidator<GetPetByIdQuery> validator)
    {
        _volunteerReadDbContext = volunteerReadDbContext;
        _validator = validator;
    }

    public async Task<Result<PetDto, ErrorList>> Handle(GetPetByIdQuery query, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }

        var pet = await _volunteerReadDbContext.Pets
            .FirstOrDefaultAsync(v => v.Id == query.PetId, cancellationToken);
        if (pet == null)
        {
            return Errors.General.NotFound(query.PetId).ToErrorList();
        }

        return pet;
    }
}