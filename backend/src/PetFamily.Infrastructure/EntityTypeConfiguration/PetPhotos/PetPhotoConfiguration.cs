using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.PetPhotos;
using PetFamily.Domain.SeedWork;

namespace PetFamily.Infrastructure.EntityTypeConfiguration.PetPhotos;

public class PetPhotoConfiguration
    : IEntityTypeConfiguration<PetPhoto>
{
    public void Configure(EntityTypeBuilder<PetPhoto> builder)
    {
        builder.ToTable("pet_photos");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => PetPhotoId.Create(value));

        builder.ComplexProperty(x => x.Path, p =>
        {
            p.Property(x => x.Value)
                .IsRequired()
                .HasMaxLength(ConfigurationConstraint.AVERAGE_TEXT_LENGTH)
                .HasColumnName("path");
        });

        builder.Property(x => x.IsMain).IsRequired();
    }
}