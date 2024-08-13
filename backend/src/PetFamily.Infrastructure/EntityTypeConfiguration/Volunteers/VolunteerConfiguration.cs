using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.SeedWork;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Infrastructure.EntityTypeConfiguration.Volunteers;

public class VolunteerConfiguration
    : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("volunteers");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.Description).IsRequired().HasMaxLength(ConfigurationConstraint.MAX_TEXT_LENGTH);
        builder.Property(x => x.Experience).IsRequired();
        builder.Property(x => x.Phone).IsRequired().HasMaxLength(ConfigurationConstraint.MIN20_TEXT_LENGTH);

        builder.Ignore(x => x.PetsAdoptedCount);
        builder.Ignore(x => x.PetsInSearchCount);
        builder.Ignore(x => x.PetsOnTreatment);

        builder.OwnsOne(x => x.FullName, fn =>
        {
            fn.Property(x => x.FirstName).IsRequired().HasMaxLength(ConfigurationConstraint.MIN20_TEXT_LENGTH);
            fn.Property(x => x.Surname).IsRequired().HasMaxLength(ConfigurationConstraint.MIN20_TEXT_LENGTH);
            fn.Property(x => x.Patronymic).IsRequired().HasMaxLength(ConfigurationConstraint.MIN20_TEXT_LENGTH);
        });

        builder.OwnsOne(x => x.SocialNetworks, sd =>
        {
            sd.ToJson();

            sd.OwnsMany(x => x.SocialNetworks, sn =>
            {
                sn.Property(x => x.Name).IsRequired().HasMaxLength(ConfigurationConstraint.MIN20_TEXT_LENGTH);
                sn.Property(x => x.Link).IsRequired().HasMaxLength(ConfigurationConstraint.AVERAGE_TEXT_LENGTH);
            });
        });

        builder.OwnsOne(x => x.Requisites, rd =>
        {
            rd.ToJson();

            rd.OwnsMany(x => x.Requisites, rq =>
            {
                rq.Property(x => x.Name).IsRequired().HasMaxLength(ConfigurationConstraint.MIN20_TEXT_LENGTH);
                rq.Property(x => x.Description).IsRequired().HasMaxLength(ConfigurationConstraint.AVERAGE_TEXT_LENGTH);
            });
        });

        builder.HasMany(x => x.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id")
            .OnDelete(DeleteBehavior.Cascade);
    }
}