using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;
using PetFamily.SpeciesManagement.Application.Database;
using PetFamily.SpeciesManagement.Application.Dtos;

namespace PetFamily.SpeciesManagement.Application.Queries.GetBreedsBySpeciesId;

public class GetBreedsBySpeciesIdHandler
    : IQueryHandler<GetBreedsBySpeciesIdQuery, IReadOnlyList<BreedDto>>
{
    private readonly ISpeciesReadDbContext _readDbContext;
    private readonly IValidator<GetBreedsBySpeciesIdQuery> _validator;

    public GetBreedsBySpeciesIdHandler(ISpeciesReadDbContext readDbContext,
        IValidator<GetBreedsBySpeciesIdQuery> validator)
    {
        _readDbContext = readDbContext;
        _validator = validator;
    }

    public async Task<Result<IReadOnlyList<BreedDto>, ErrorList>> Handle(GetBreedsBySpeciesIdQuery query,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }

        var breeds = await _readDbContext.Breeds.Where(x => x.SpeciesId == query.SpeciesId)
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);

        return breeds;
    }
}