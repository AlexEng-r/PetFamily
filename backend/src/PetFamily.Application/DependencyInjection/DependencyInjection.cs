using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Volunteers.CreateVolunteer;

namespace PetFamily.Application.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationInject(this IServiceCollection collection)
    {
        collection.AddScoped<CreateVolunteersService>();

        return collection;
    }
}