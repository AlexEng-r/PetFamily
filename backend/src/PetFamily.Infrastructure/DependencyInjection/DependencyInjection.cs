using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PetFamily.Application.Database;
using PetFamily.Application.MessageQueues;
using PetFamily.Application.Providers;
using PetFamily.Application.Providers.Crypto;
using PetFamily.Application.Providers.File;
using PetFamily.Application.Repositories.Specieses;
using PetFamily.Application.Repositories.Volunteers;
using PetFamily.Application.Services;
using PetFamily.Infrastructure.BackgroundServices;
using PetFamily.Infrastructure.DatabaseContexts;
using PetFamily.Infrastructure.MessageQueues;
using PetFamily.Infrastructure.Options;
using PetFamily.Infrastructure.Providers;
using PetFamily.Infrastructure.Repositories;
using PetFamily.Infrastructure.Services;
using PetFamily.Infrastructure.SqlConnection;
using FileInfo = PetFamily.Application.Providers.File.FileInfo;

namespace PetFamily.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureInject(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddDbContexts(configuration)
            .AddDatabase(configuration)
            .AddMinio(configuration)
            .AddRepositories(configuration)
            .AddHostedServices(configuration);

        services.AddScoped<IFileCleanerService, FileCleanerService>();
        services.AddSingleton<IMessageQueue<IEnumerable<FileInfo>>, InMemoryMessageQueue<IEnumerable<FileInfo>>>();
        services.AddScoped<ICryptoProvider, CryptoProvider>();

        return services;
    }

    private static IServiceCollection AddDbContexts(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<WriteDbContext>();
        services.AddScoped<IReadDbContext, ReadDbContext>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IVolunteersRepository, VolunteersRepository>();
        services.AddScoped<ISpeciesRepository, SpeciesRepository>();

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();

        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        return services;
    }

    private static IServiceCollection AddHostedServices(this IServiceCollection services,
        IConfiguration configuration)
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