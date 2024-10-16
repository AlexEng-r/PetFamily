using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Application.Dtos;

namespace PetFamily.Infrastructure.EntityTypeConfiguration.Read.Species;

public class SpeciesDtoConfiguration
    : IEntityTypeConfiguration<SpeciesDto>
{
    public void Configure(EntityTypeBuilder<SpeciesDto> builder)
    {
        builder.ToTable("species");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.IsDeleted).HasColumnName("is_Deleted");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}