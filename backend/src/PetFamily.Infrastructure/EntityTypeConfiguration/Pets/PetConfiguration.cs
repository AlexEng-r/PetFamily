using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Pets;
using PetFamily.Domain.SeedWork;

namespace PetFamily.Infrastructure.EntityTypeConfiguration.Pets;

public class PetConfiguration
    : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.NickName).IsRequired().HasMaxLength(ConfigurationConstraint.MIN20_TEXT_LENGTH);
        builder.Property(x => x.AnimalType).IsRequired().HasMaxLength(ConfigurationConstraint.MIN50_TEXT_LENGTH);
        builder.Property(x => x.Description).HasMaxLength(ConfigurationConstraint.MAX_TEXT_LENGTH);
        builder.Property(x => x.Breed).HasMaxLength(ConfigurationConstraint.MIN50_TEXT_LENGTH);
        builder.Property(x => x.Color).IsRequired().HasMaxLength(ConfigurationConstraint.MIN20_TEXT_LENGTH);
        builder.Property(x => x.HealthInformation).HasMaxLength(ConfigurationConstraint.MIN50_TEXT_LENGTH);
        builder.Property(x => x.Address).IsRequired().HasMaxLength(ConfigurationConstraint.AVERAGE_TEXT_LENGTH);
        builder.Property(x => x.Phone).IsRequired().HasMaxLength(ConfigurationConstraint.MIN20_TEXT_LENGTH);
        builder.Property(x => x.IsSterialized).IsRequired();
        builder.Property(x => x.IsVaccinated).IsRequired();
        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(ConfigurationConstraint.MIN20_TEXT_LENGTH);

        builder.Property(x => x.DateCreated).IsRequired();

        builder.HasMany(x => x.PetPhotos)
            .WithOne()
            .HasForeignKey("pet_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsOne(x => x.Requisites, p =>
        {
            p.ToJson();

            p.OwnsMany(x => x.Requisites, rq =>
            {
                rq.Property(x => x.Name).IsRequired().HasMaxLength(ConfigurationConstraint.MIN20_TEXT_LENGTH);
                rq.Property(x => x.Description).IsRequired().HasMaxLength(ConfigurationConstraint.AVERAGE_TEXT_LENGTH);
            });
        });
    }
}