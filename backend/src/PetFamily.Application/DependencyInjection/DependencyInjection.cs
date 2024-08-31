using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Volunteers.Create;
using PetFamily.Application.Volunteers.Delete;
using PetFamily.Application.Volunteers.UpdateMainInfo;
using PetFamily.Application.Volunteers.UpdateRequisites;
using PetFamily.Application.Volunteers.UpdateSocialNetworks;

namespace PetFamily.Application.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationInject(this IServiceCollection collection)
    {
        collection.AddScoped<CreateVolunteersHandler>();
        collection.AddScoped<UpdateMainInfoHandler>();
        collection.AddScoped<UpdateSocialNetworkHandler>();
        collection.AddScoped<UpdateRequisiteHandler>();
        collection.AddScoped<DeleteVolunteerHandler>();

        collection.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        return collection;
    }
}