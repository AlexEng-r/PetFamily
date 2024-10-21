﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Application.Dtos;
using PetFamily.Infrastructure.Extensions;

namespace PetFamily.Infrastructure.EntityTypeConfiguration.Read.Pets;

public class PetDtoConfiguration
    : IEntityTypeConfiguration<PetDto>
{
    public void Configure(EntityTypeBuilder<PetDto> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Requisites)
            .ValueObjectDtoCollectionJsonConversion();
        builder.Property(x => x.IsDeleted).HasColumnName("is_Deleted");

        builder.ComplexProperty(x => x.Address, a =>
        {
            a.IsRequired();

            a.Property(x => x.City)
                .HasColumnName("city");

            a.Property(x => x.House)
                .HasColumnName("house");

            a.Property(x => x.Flat)
                .HasColumnName("flat");
        });

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}