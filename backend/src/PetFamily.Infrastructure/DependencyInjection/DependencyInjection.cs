using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Repositories.Volunteers;
using PetFamily.Infrastructure.Repositories;

namespace PetFamily.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureInject(this IServiceCollection collection)
    {
        collection.AddScoped<IVolunteersRepository, VolunteersRepository>();

        return collection;
    }
}