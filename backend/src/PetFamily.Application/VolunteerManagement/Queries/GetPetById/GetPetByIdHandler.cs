using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Application.Extensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.VolunteerManagement.Queries.GetPetById;

public class GetPetByIdHandler
    : IQueryHandler<GetPetByIdQuery, PetDto>
{
    private readonly IReadDbContext _readDbContext;
    private readonly IValidator<GetPetByIdQuery> _validator;

    public GetPetByIdHandler(IReadDbContext readDbContext,
        IValidator<GetPetByIdQuery> validator)
    {
        _readDbContext = readDbContext;
        _validator = validator;
    }

    public async Task<Result<PetDto, ErrorList>> Handle(GetPetByIdQuery query, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }

        var pet = await _readDbContext.Pets
            .FirstOrDefaultAsync(v => v.Id == query.PetId, cancellationToken);
        if (pet == null)
        {
            return Errors.General.NotFound(query.PetId).ToErrorList();
        }

        return pet;
    }
}