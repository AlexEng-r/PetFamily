using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.MessageQueues;
using PetFamily.Application.VolunteerManagement.Commands.AddPetPhoto;
using PetFamily.Application.VolunteerManagement.Commands.AddPets;
using PetFamily.Application.VolunteerManagement.Commands.ChangePetPosition;
using PetFamily.Application.VolunteerManagement.Commands.Create;
using PetFamily.Application.VolunteerManagement.Commands.Delete;
using PetFamily.Application.VolunteerManagement.Commands.UpdateMainInfo;
using PetFamily.Application.VolunteerManagement.Commands.UpdateRequisites;
using PetFamily.Application.VolunteerManagement.Commands.UpdateSocialNetworks;
using PetFamily.Application.VolunteerManagement.Queries.GetVolunteersWithPagination;
using FileInfo = PetFamily.Application.Providers.FileInfo;

namespace PetFamily.Application.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationInject(this IServiceCollection services)
    {
        services
            .AddCommandHandlers()
            .AddQueryHandlers()
            .AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }

    private static IServiceCollection AddCommandHandlers(this IServiceCollection services)
        => services.Scan(scan => scan
            .FromAssemblies(typeof(DependencyInjection).Assembly)
            .AddClasses(classes => classes.AssignableToAny(typeof(ICommandHandler<,>), typeof(ICommandHandler<>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

    private static IServiceCollection AddQueryHandlers(this IServiceCollection services)
        => services.Scan(scan => scan
            .FromAssemblies(typeof(DependencyInjection).Assembly)
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());
}