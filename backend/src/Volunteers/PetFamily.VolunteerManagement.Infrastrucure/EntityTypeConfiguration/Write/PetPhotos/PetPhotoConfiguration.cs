using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects.Ids;
using PetFamily.VolunteerManagement.Domain.Entities.PetPhotos;

namespace PetFamily.VolunteerManagement.Infrastrucure.EntityTypeConfiguration.Write.PetPhotos;

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
        
        builder.Property(x => x.HashCode).IsRequired();

        builder.Property(x => x.IsMain).IsRequired();
    }
}