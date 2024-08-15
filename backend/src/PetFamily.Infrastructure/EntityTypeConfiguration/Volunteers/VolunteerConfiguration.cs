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

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => VolunteerId.Create(value));

        builder.Property(x => x.Experience).IsRequired();

        builder.Ignore(x => x.PetsAdoptedCount);
        builder.Ignore(x => x.PetsInSearchCount);
        builder.Ignore(x => x.PetsOnTreatment);

        builder.ComplexProperty(x => x.FullName, fn =>
        {
            fn.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(ConfigurationConstraint.MIN20_TEXT_LENGTH)
                .HasColumnName("firstname");

            fn.Property(x => x.Surname)
                .IsRequired()
                .HasMaxLength(ConfigurationConstraint.MIN20_TEXT_LENGTH)
                .HasColumnName("surname");

            fn.Property(x => x.Patronymic)
                .IsRequired()
                .HasMaxLength(ConfigurationConstraint.MIN20_TEXT_LENGTH)
                .HasColumnName("patronymic");
        });

        builder.ComplexProperty(x => x.Description, d =>
        {
            d.Property(x => x.Value)
                .IsRequired()
                .HasMaxLength(ConfigurationConstraint.MAX_TEXT_LENGTH)
                .HasColumnName("description");
        });

        builder.ComplexProperty(x => x.Phone, p =>
        {
            p.Property(x => x.Value)
                .IsRequired()
                .HasMaxLength(ConfigurationConstraint.MIN20_TEXT_LENGTH)
                .HasColumnName("phone");
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