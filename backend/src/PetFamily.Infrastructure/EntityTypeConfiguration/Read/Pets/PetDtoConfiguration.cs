using Microsoft.EntityFrameworkCore;
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
    }
}