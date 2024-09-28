﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;

namespace PetFamily.Infrastructure.DatabaseContexts;

public class ReadDbContext
    : DbContext, IReadDbContext
{
    private readonly IConfiguration _configuration;

    public ReadDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IQueryable<VolunteerDto> Volunteers => Set<VolunteerDto>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(_configuration.GetConnectionString("Database"))
            .UseSnakeCaseNamingConvention()
            .UseLoggerFactory(CreateLoggerFactory());

        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WriteDbContext).Assembly,
            type => type.FullName?.Contains("Configuration.Read") ?? false);
    }

    private ILoggerFactory CreateLoggerFactory()
        => LoggerFactory.Create(builder => { builder.AddConsole(); });
}