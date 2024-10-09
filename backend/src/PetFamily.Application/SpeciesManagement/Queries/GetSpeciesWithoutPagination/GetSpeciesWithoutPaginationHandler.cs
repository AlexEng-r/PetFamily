using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.SpeciesManagement.Queries.GetSpeciesWithoutPagination;

public class GetSpeciesWithoutPaginationHandler
    : IQueryHandler<IReadOnlyList<SpeciesDto>>
{
    private readonly IReadDbContext _readDbContext;

    public GetSpeciesWithoutPaginationHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<IReadOnlyList<SpeciesDto>, ErrorList>> Handle(CancellationToken cancellationToken)
        => await _readDbContext.Species.OrderBy(x => x.Name).ToListAsync(cancellationToken);
}