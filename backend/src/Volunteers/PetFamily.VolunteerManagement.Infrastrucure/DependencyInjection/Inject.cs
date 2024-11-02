using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PetFamily.Core.BackgroundServices;
using PetFamily.Core.MessageQueues;
using PetFamily.Core.Providers.Crypto;
using PetFamily.Core.Providers.File;
using PetFamily.Core.Services;
using PetFamily.VolunteerManagement.Application;
using PetFamily.VolunteerManagement.Application.Database;
using PetFamily.VolunteerManagement.Application.Repositories;
using PetFamily.VolunteerManagement.Infrastrucure.DatabaseContexts;
using PetFamily.VolunteerManagement.Infrastrucure.Options;
using PetFamily.VolunteerManagement.Infrastrucure.Providers;
using PetFamily.VolunteerManagement.Infrastrucure.Repositories;
using PetFamily.VolunteerManagement.Infrastrucure.SqlConnection;
using FileInfo = PetFamily.Core.Providers.File.FileInfo;

namespace PetFamily.VolunteerManagement.Infrastrucure.DependencyInjection;

public static class Inject
{
    public static IServiceCollection AddVolunteerInfrastructureInject(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddDbContexts()
            .AddDatabase()
            .AddMinio(configuration)
            .AddRepositories()
            .AddHostedServices();

        services.AddScoped<IFileCleanerService, FileCleanerService>();
        services.AddSingleton<IMessageQueue<IEnumerable<FileInfo>>, InMemoryMessageQueue<IEnumerable<FileInfo>>>();
        services.AddScoped<ICryptoProvider, CryptoProvider>();

        return services;
    }

    private static IServiceCollection AddDbContexts(this IServiceCollection services)
    {
        services.AddScoped<VolunteerWriteDbContext>();
        services.AddScoped<IVolunteerReadDbContext, VolunteerReadDbContext>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IVolunteersRepository, VolunteersRepository>();

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddScoped<IVolunteerUnitOfWork, VolunteerUnitOfWork>();
        services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();

        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        return services;
    }

    private static IServiceCollection AddHostedServices(this IServiceCollection services)
    {
        services.AddHostedService<FileCleanerBackgroundService>();

        return services;
    }

    private static IServiceCollection AddMinio(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<MinioOptions>(configuration.GetSection(MinioOptions.SECTION_NAME));

        services.AddMinio(options =>
        {
            var minioOptions = configuration.GetSection(MinioOptions.SECTION_NAME).Get<MinioOptions>() ??
                               throw new ApplicationException("Missing minio configuration");

            options.WithEndpoint(minioOptions.Endpoint);
            options.WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey);
            options.WithSSL(minioOptions.EnableSSl);
        });

        services.AddScoped<IFileProvider, MinioProvider>();

        return services;
    }
}