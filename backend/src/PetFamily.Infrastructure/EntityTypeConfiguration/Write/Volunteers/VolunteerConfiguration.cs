using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared;
using PetFamily.Domain.VolunteerManagement.Volunteers;
using PetFamily.Infrastructure.Extensions;

namespace PetFamily.Infrastructure.EntityTypeConfiguration.Write.Volunteers;

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
                .IsRequired(false)
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
            p.Property(x => x.Phone)
                .IsRequired()
                .HasMaxLength(ConfigurationConstraint.MIN20_TEXT_LENGTH)
                .HasColumnName("phone");
        });

        builder.Property(x => x.SocialNetworks)
            .ValueObjectCollectionJsonConversion();

        builder.Property(x => x.Requisites)
            .ValueObjectCollectionJsonConversion();

        builder.HasMany(x => x.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.IsDeleted)
            .HasColumnName("is_Deleted");
        
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}