using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.VolunteerManagement.Application.DependencyInjection;
using PetFamily.VolunteerManagement.Contracts;
using PetFamily.VolunteerManagement.Infrastrucure.DependencyInjection;

namespace PetFamily.VolunteerManagement.Presentation.DependencyInjection;

public static class Inject
{
    public static IServiceCollection AddVolunteerManagement(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddScoped<IVolunteerContract, VolunteerContract>();
            
            services
            .AddVolunteerApplicationInject()
            .AddVolunteerInfrastructureInject(configuration);

        return services;
    }
}