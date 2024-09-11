using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PetFamily.Application.Database;
using PetFamily.Application.Providers;
using PetFamily.Application.Repositories.Volunteers;
using PetFamily.Infrastructure.Options;
using PetFamily.Infrastructure.Providers;
using PetFamily.Infrastructure.Repositories;

namespace PetFamily.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureInject(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IVolunteersRepository, VolunteersRepository>();

        services.AddMinio(configuration);

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