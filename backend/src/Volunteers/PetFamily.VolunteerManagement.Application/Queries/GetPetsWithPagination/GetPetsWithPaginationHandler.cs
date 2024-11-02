using System.Linq.Expressions;
using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.Core.Models;
using PetFamily.SharedKernel;
using PetFamily.VolunteerManagement.Application.Database;
using PetFamily.VolunteerManagement.Application.Dtos;

namespace PetFamily.VolunteerManagement.Application.Queries.GetPetsWithPagination;

public class GetPetsWithPaginationHandler
    : IQueryHandler<GetPetsWithPaginationQuery, PagedList<PetDto>>
{
    private readonly IVolunteerReadDbContext _volunteerReadDbContext;
    private readonly IValidator<GetPetsWithPaginationQuery> _validator;

    public GetPetsWithPaginationHandler(IVolunteerReadDbContext volunteerReadDbContext,
        IValidator<GetPetsWithPaginationQuery> validator)
    {
        _volunteerReadDbContext = volunteerReadDbContext;
        _validator = validator;
    }

    public async Task<Result<PagedList<PetDto>, ErrorList>> Handle(GetPetsWithPaginationQuery query, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }

        var keySelector = SortByProperty(query.SortBy);

        var petQuery = _volunteerReadDbContext.Pets;

        petQuery = petQuery
            .WhereIf(query.NickName != null, x => x.NickName == query.NickName)
            .WhereIf(query.IsSterialized.HasValue, x => x.IsSterialized == query.IsSterialized)
            .WhereIf(query.IsVaccinated.HasValue, x => x.IsVaccinated == query.IsVaccinated)
            .WhereIf(query.VolunteerId.HasValue, x => x.VolunteerId == query.VolunteerId)
            .WhereIf(query.Species != null, x => x.SpeciesId == query.Species)
            .WhereIf(query.Breed != null, x => x.BreedId == query.Breed)
            .WhereIf(query.VolunteerId.HasValue, x => x.VolunteerId == query.VolunteerId)
            .WhereIf(query.Status != null, x => x.Status == query.Status);
        
        petQuery = query.SortDirection?.ToLower() == "desc"
            ? petQuery.OrderByDescending(keySelector)
            : petQuery.OrderBy(keySelector);

        return await petQuery
            .ToPagedList(query.Page, query.PageSize, cancellationToken);
    }

    private static Expression<Func<PetDto, object>> SortByProperty(string? sortBy)
    {
        if (string.IsNullOrEmpty(sortBy))
            return pet => pet.NickName;

        Expression<Func<PetDto, object>> keySelector = sortBy.ToLower() switch
        {
            "nickname" => pet => pet.NickName,
            "species" => pet => pet.SpeciesId,
            "breed" => pet => pet.BreedId,

            _ => pet => pet.NickName,
        };
        return keySelector;
    }
}