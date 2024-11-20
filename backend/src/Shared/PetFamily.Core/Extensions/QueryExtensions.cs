using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Core.Models;

namespace PetFamily.Core.Extensions;

public static class QueryExtensions
{
    public static async Task<PagedList<T>> ToPagedList<T>(this IQueryable<T> source, int page,
        int pageSize, CancellationToken cancellationToken)
    {
        var totalCount = await source.CountAsync(cancellationToken);

        var entities = await source
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedList<T>()
        {
            Items = entities,
            PageSize = page,
            Page = pageSize,
            TotalCount = totalCount
        };
    }

    public static IQueryable<T> WhereIf<T>(this IQueryable<T> source,
        bool condition,
        Expression<Func<T, bool>> predicate)
    {
        return condition ? source.Where(predicate) : source;
    }
}