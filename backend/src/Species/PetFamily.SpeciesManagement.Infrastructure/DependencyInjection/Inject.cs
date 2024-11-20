using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.SpeciesManagement.Application;
using PetFamily.SpeciesManagement.Application.Database;
using PetFamily.SpeciesManagement.Application.Repositories;
using PetFamily.SpeciesManagement.Infrastructure.DatabaseContexts;
using PetFamily.SpeciesManagement.Infrastructure.Repositories;

namespace PetFamily.SpeciesManagement.Infrastructure.DependencyInjection;

public static class Inject
{
    public static IServiceCollection AddSpeciesInfrastructureInject(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddDbContexts()
            .AddDatabase()
            .AddRepositories();

        return services;
    }

    private static IServiceCollection AddDbContexts(this IServiceCollection services)
    {
        services.AddScoped<SpeciesWriteDbContext>();
        services.AddScoped<ISpeciesReadDbContext, SpeciesReadDbContext>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ISpeciesRepository, SpeciesRepository>();

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddScoped<ISpeciesUnitOfWork, SpeciesUnitOfWork>();

        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        return services;
    }
}