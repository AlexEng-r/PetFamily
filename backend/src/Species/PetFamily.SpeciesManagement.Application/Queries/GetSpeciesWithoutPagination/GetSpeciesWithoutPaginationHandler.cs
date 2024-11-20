using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos;
using PetFamily.SharedKernel;
using PetFamily.SpeciesManagement.Application.Database;

namespace PetFamily.SpeciesManagement.Application.Queries.GetSpeciesWithoutPagination;

public class GetSpeciesWithoutPaginationHandler
    : IQueryHandler<IReadOnlyList<SpeciesDto>>
{
    private readonly ISpeciesReadDbContext _readDbContext;

    public GetSpeciesWithoutPaginationHandler(ISpeciesReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<IReadOnlyList<SpeciesDto>, ErrorList>> Handle(CancellationToken cancellationToken)
        => await _readDbContext.Species.OrderBy(x => x.Name).ToListAsync(cancellationToken);
}