using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.SpeciesManagement.Specieses;
using PetFamily.Domain.VolunteerManagement.Volunteers;

namespace PetFamily.Infrastructure.DatabaseContexts;

public class WriteDbContext
    : DbContext
{
    private readonly IConfiguration _configuration;

    public WriteDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<Volunteer> Volunteers => Set<Volunteer>();

    public DbSet<Species> Species => Set<Species>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(_configuration.GetConnectionString("Database"))
            .UseSnakeCaseNamingConvention()
            .UseLoggerFactory(CreateLoggerFactory());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WriteDbContext).Assembly,
            type => type.FullName?.Contains("Configuration.Write") ?? false);
    }

    private ILoggerFactory CreateLoggerFactory()
        => LoggerFactory.Create(builder => { builder.AddConsole(); });
}