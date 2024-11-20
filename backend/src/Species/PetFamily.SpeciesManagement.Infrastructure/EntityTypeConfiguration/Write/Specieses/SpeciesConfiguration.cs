﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects.Ids;
using PetFamily.SpeciesManagement.Domain;

namespace PetFamily.SpeciesManagement.Infrastructure.EntityTypeConfiguration.Write.Specieses;

public class SpeciesConfiguration
    : IEntityTypeConfiguration<Species>
{
    public void Configure(EntityTypeBuilder<Species> builder)
    {
        builder.ToTable("species");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => SpeciesId.Create(value));

        builder.ComplexProperty(x => x.Name, n =>
        {
            n.Property(x => x.Value)
                .IsRequired()
                .HasMaxLength(ConfigurationConstraint.MIN20_TEXT_LENGTH)
                .HasColumnName("name");
        });

        builder.HasMany(x => x.Breeds)
            .WithOne()
            .HasForeignKey("species_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.IsDeleted)
            .HasColumnName("is_Deleted");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}