using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Application.Dtos;

namespace PetFamily.Infrastructure.EntityTypeConfiguration.Read.PetPhotos;

public class PetPhotoDtoConfiguration
    : IEntityTypeConfiguration<PetPhotoDto>
{
    public void Configure(EntityTypeBuilder<PetPhotoDto> builder)
    {
        builder.ToTable("pet_photos");
        builder.HasKey(x => x.Id);
    }
}