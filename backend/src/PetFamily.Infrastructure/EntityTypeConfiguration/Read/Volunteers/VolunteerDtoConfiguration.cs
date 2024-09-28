﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Application.Dtos;
using PetFamily.Domain.Shared;

namespace PetFamily.Infrastructure.EntityTypeConfiguration.Read.Volunteers;

public class VolunteerDtoConfiguration
    : IEntityTypeConfiguration<VolunteerDto>
{
    public void Configure(EntityTypeBuilder<VolunteerDto> builder)
    {
        builder.ToTable("volunteers");

        builder.HasKey(x => x.Id);

        builder.ComplexProperty(x => x.FullName, fn =>
        {
            fn.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(ConfigurationConstraint.MIN20_TEXT_LENGTH)
                .HasColumnName("firstname");

            fn.Property(x => x.Surname)
                .IsRequired()
                .HasMaxLength(ConfigurationConstraint.MIN20_TEXT_LENGTH)
                .HasColumnName("surname");

            fn.Property(x => x.Patronymic)
                .IsRequired(false)
                .HasMaxLength(ConfigurationConstraint.MIN20_TEXT_LENGTH)
                .HasColumnName("patronymic");
        });
    }
}