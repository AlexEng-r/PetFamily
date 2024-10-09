using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using CSharpFunctionalExtensions;
using Dapper;
using FluentValidation;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Application.Extensions;
using PetFamily.Application.Models;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects.FullNames;

namespace PetFamily.Application.VolunteerManagement.Queries.GetVolunteersWithPagination;

public class GetVolunteersWithPaginationHandler
    : IQueryHandler<GetFilteredVolunteersWithPaginationQuery, PagedList<VolunteerDto>>
{
    private readonly IReadDbContext _readDbContext;
    private readonly IValidator<GetFilteredVolunteersWithPaginationQuery> _validator;

    public GetVolunteersWithPaginationHandler(IReadDbContext readDbContext,
        IValidator<GetFilteredVolunteersWithPaginationQuery> validator)
    {
        _readDbContext = readDbContext;
        _validator = validator;
    }

    public async Task<Result<PagedList<VolunteerDto>, ErrorList>> Handle(GetFilteredVolunteersWithPaginationQuery query,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }

        var keySelector = SortByProperty(query.SortBy);

        var volunteerQuery = _readDbContext.Volunteers;

        volunteerQuery = volunteerQuery
            .WhereIf(query.Experience.HasValue, x => x.Experience == query.Experience!.Value);

        volunteerQuery = query.SortDirection?.ToLower() == "desc"
            ? volunteerQuery.OrderByDescending(keySelector)
            : volunteerQuery.OrderBy(keySelector);

        return await volunteerQuery
            .ToPagedList(query.Page, query.PageSize, cancellationToken);
    }

    private static Expression<Func<VolunteerDto, object>> SortByProperty(string? sortBy)
    {
        if (string.IsNullOrEmpty(sortBy))
            return volunteer => volunteer.FullName.Surname;

        Expression<Func<VolunteerDto, object>> keySelector = sortBy.ToLower() switch
        {
            "name" => volunteer => volunteer.FullName.FirstName,
            "surname" => volunteer => volunteer.FullName.Surname,
            "exp" => volunteer => volunteer.Experience,
            _ => volunteer => volunteer.FullName.Surname
        };
        return keySelector;
    }
}

public class GetVolunteersWithPaginationHandlerDapper
    : IQueryHandler<GetFilteredVolunteersWithPaginationQuery, PagedList<VolunteerDto>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetVolunteersWithPaginationHandlerDapper(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<PagedList<VolunteerDto>, ErrorList>> Handle(GetFilteredVolunteersWithPaginationQuery query,
        CancellationToken cancellationToken)
    {
        var connection = _sqlConnectionFactory.Create();

        var parameters = new DynamicParameters();

        var totalCount = await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM volunteers");

        var sql = new StringBuilder("""
                                    SELECT id,
                                           experience,
                                           description,
                                           phone,
                                           firstname,
                                           surname,
                                           patronymic,
                                           social_networks,
                                           requisites
                                    FROM volunteers
                                    """);

        if (query.Experience.HasValue)
        {
            sql.Append(" WHERE experience = @Experience");
            parameters.Add("@Experience", query.Experience.Value);
        }

        sql.ApplySorting(query.SortBy, query.SortDirection);
        sql.ApplyPagination(parameters, query.Page, query.PageSize);

        var a = sql.ToString();
        var volunteer = await connection
            .QueryAsync<VolunteerDto, FullNameDto, string, string, VolunteerDto>(a,
                (volunteer, fullName, socialNetworks, requisites) =>
                {
                    volunteer.FullName = fullName;
                    volunteer.SocialNetworks =
                        JsonSerializer.Deserialize<IReadOnlyList<SocialNetworksDto>>(socialNetworks) ??
                        new List<SocialNetworksDto>();
                    volunteer.Requisites = JsonSerializer.Deserialize<IReadOnlyList<RequisiteDto>>(requisites) ??
                                           new List<RequisiteDto>();
                    return volunteer;
                },
                splitOn: "firstName,social_networks, requisites",
                param: parameters);

        return new PagedList<VolunteerDto>
        {
            Items = volunteer.ToList(),
            PageSize = query.PageSize,
            Page = query.Page,
            TotalCount = totalCount,
        };
    }
}

public static class SqlExtension
{
    public static void ApplySorting(
        this StringBuilder builder,
        string? sortBy,
        string? sortDirection)
    {
        var sortByValid = sortBy ?? "id";
        var sortDirectionValid = sortDirection ?? "asc";

        builder.Append($" ORDER BY {sortByValid} {sortDirectionValid}");
    }

    public static void ApplyPagination(
        this StringBuilder builder,
        DynamicParameters parameters,
        int page,
        int pageSize)
    {
        parameters.Add("@PageSize", pageSize);
        parameters.Add("@Offset", (page - 1) * pageSize);

        builder.Append(" LIMIT @PageSize OFFSET @Offset");
    }
}