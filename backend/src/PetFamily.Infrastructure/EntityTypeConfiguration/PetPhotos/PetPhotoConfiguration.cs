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

        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.Path).IsRequired().HasMaxLength(ConfigurationConstraint.AVERAGE_TEXT_LENGTH);
        builder.Property(x => x.IsMain).IsRequired();
    }
}