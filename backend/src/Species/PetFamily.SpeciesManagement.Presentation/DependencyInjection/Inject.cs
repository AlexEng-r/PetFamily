

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.SpeciesManagement.Application.DependencyInjection;
using PetFamily.SpeciesManagement.Contracts;
using PetFamily.SpeciesManagement.Infrastructure.DependencyInjection;

namespace PetFamily.SpeciesManagement.Presentation.DependencyInjection;

public static class Inject
{
    public static IServiceCollection AddSpeciesManagement(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddScoped<ISpeciesContract, SpeciesContract>()
            .AddSpeciesApplicationInject()
            .AddSpeciesInfrastructureInject(configuration);

        return services;
    }
}