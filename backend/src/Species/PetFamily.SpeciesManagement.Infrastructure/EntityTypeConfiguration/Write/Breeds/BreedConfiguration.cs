using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects.Ids;
using PetFamily.SpeciesManagement.Domain.Entities.Breeds;

namespace PetFamily.SpeciesManagement.Infrastructure.EntityTypeConfiguration.Write.Breeds;

public class BreedConfiguration
    : IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder.ToTable("breeds");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => BreedId.Create(value));

        builder.ComplexProperty(x => x.Name, n =>
        {
            n.Property(x => x.Value)
                .IsRequired()
                .HasMaxLength(ConfigurationConstraint.MIN20_TEXT_LENGTH)
                .HasColumnName("name");
        });

        builder.Property(x => x.IsDeleted)
            .HasColumnName("is_Deleted");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}