using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Breeds;
using PetFamily.Domain.SeedWork;

namespace PetFamily.Infrastructure.EntityTypeConfiguration.Breeds;

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
    }
}