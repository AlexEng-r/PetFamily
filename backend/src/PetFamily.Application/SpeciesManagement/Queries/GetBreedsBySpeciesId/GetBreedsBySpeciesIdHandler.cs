using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Application.Extensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.SpeciesManagement.Queries.GetBreedsBySpeciesId;

public class GetBreedsBySpeciesIdHandler
    : IQueryHandler<GetBreedsBySpeciesIdQuery, IReadOnlyList<BreedDto>>
{
    private readonly IReadDbContext _readDbContext;
    private readonly IValidator<GetBreedsBySpeciesIdQuery> _validator;

    public GetBreedsBySpeciesIdHandler(IReadDbContext readDbContext,
        IValidator<GetBreedsBySpeciesIdQuery> validator)
    {
        _readDbContext = readDbContext;
        _validator = validator;
    }

    public async Task<Result<IReadOnlyList<BreedDto>, ErrorList>> Handle(GetBreedsBySpeciesIdQuery command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }

        var breeds = await _readDbContext.Breeds.Where(x => x.SpeciesId == command.SpeciesId)
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);

        return breeds;
    }
}