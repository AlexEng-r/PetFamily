using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstractions;

namespace PetFamily.SpeciesManagement.Application.DependencyInjection;

public static class Inject
{
    public static IServiceCollection AddSpeciesApplicationInject(this IServiceCollection services)
    {
        services
            .AddCommandHandlers()
            .AddQueryHandlers()
            .AddValidatorsFromAssembly(typeof(Inject).Assembly);

        return services;
    }

    private static IServiceCollection AddCommandHandlers(this IServiceCollection services)
        => services.Scan(scan => scan
            .FromAssemblies(typeof(Inject).Assembly)
            .AddClasses(classes => classes.AssignableToAny(typeof(ICommandHandler<,>), typeof(ICommandHandler<>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

    private static IServiceCollection AddQueryHandlers(this IServiceCollection services)
        => services.Scan(scan => scan
            .FromAssemblies(typeof(Inject).Assembly)
            .AddClasses(classes => classes.AssignableToAny(typeof(IQueryHandler<,>), typeof(IQueryHandler<>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());
}