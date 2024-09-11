using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Volunteers.AddPetPhoto;
using PetFamily.Application.Volunteers.AddPets;
using PetFamily.Application.Volunteers.Create;
using PetFamily.Application.Volunteers.Delete;
using PetFamily.Application.Volunteers.UpdateMainInfo;
using PetFamily.Application.Volunteers.UpdateRequisites;
using PetFamily.Application.Volunteers.UpdateSocialNetworks;

namespace PetFamily.Application.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationInject(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteersHandler>();
        services.AddScoped<UpdateMainInfoHandler>();
        services.AddScoped<UpdateSocialNetworkHandler>();
        services.AddScoped<UpdateRequisiteHandler>();
        services.AddScoped<DeleteVolunteerHandler>();

        services.AddScoped<AddPetHandler>();
        services.AddScoped<AddPetPhotoHandler>();

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        return services;
    }
}