using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Application.Dtos;

namespace PetFamily.Infrastructure.EntityTypeConfiguration.Read.Breeds;

public class BreedDtoConfiguration
    : IEntityTypeConfiguration<BreedDto>
{
    public void Configure(EntityTypeBuilder<BreedDto> builder)
    {
        builder.ToTable("breeds");

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.SpeciesId).HasColumnName("species_id");
    }
}