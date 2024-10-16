using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Application.Dtos;
using PetFamily.Domain.Shared;
using PetFamily.Infrastructure.Extensions;

namespace PetFamily.Infrastructure.EntityTypeConfiguration.Read.Volunteers;

public class VolunteerDtoConfiguration
    : IEntityTypeConfiguration<VolunteerDto>
{
    public void Configure(EntityTypeBuilder<VolunteerDto> builder)
    {
        builder.ToTable("volunteers");

        builder.HasKey(x => x.Id);

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

        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Phone).HasColumnName("phone");
        builder.Property(x => x.IsDeleted).HasColumnName("is_Deleted");

        builder.Property(x => x.SocialNetworks)
            .ValueObjectDtoCollectionJsonConversion();

        builder.Property(x => x.Requisites)
            .ValueObjectDtoCollectionJsonConversion();

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}