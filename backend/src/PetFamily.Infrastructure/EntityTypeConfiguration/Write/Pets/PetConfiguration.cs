using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagement.Specieses;
using PetFamily.Domain.ValueObjects.Requisites;
using PetFamily.Domain.VolunteerManagement.Pets;

namespace PetFamily.Infrastructure.EntityTypeConfiguration.Write.Pets;

public class PetConfiguration
    : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => PetId.Create(value));

        builder.ComplexProperty(x => x.NickName, nn =>
        {
            nn.Property(x => x.Value)
                .IsRequired()
                .HasMaxLength(ConfigurationConstraint.MIN20_TEXT_LENGTH)
                .HasColumnName("nick_name");
        });

        builder.ComplexProperty(x => x.AnimalType, at =>
        {
            at.Property(x => x.Value)
                .IsRequired()
                .HasMaxLength(ConfigurationConstraint.MIN50_TEXT_LENGTH)
                .HasColumnName("animal_type");
        });

        builder.ComplexProperty(x => x.Description, d =>
        {
            d.Property(x => x.Value)
                .HasMaxLength(ConfigurationConstraint.MAX_TEXT_LENGTH)
                .HasColumnName("description");
        });

        builder.ComplexProperty(x => x.Breed, b =>
        {
            b.Property(x => x.Value)
                .HasMaxLength(ConfigurationConstraint.MIN50_TEXT_LENGTH)
                .HasColumnName("breed");
        });

        builder.ComplexProperty(x => x.Color, c =>
        {
            c.Property(x => x.Value)
                .IsRequired()
                .HasMaxLength(ConfigurationConstraint.MIN20_TEXT_LENGTH)
                .HasColumnName("color");
        });

        builder.ComplexProperty(x => x.HealthInformation, hi =>
        {
            hi.Property(x => x.Value)
                .HasMaxLength(ConfigurationConstraint.MIN50_TEXT_LENGTH)
                .HasColumnName("health_information");
        });

        builder.ComplexProperty(x => x.Address, a =>
        {
            a.IsRequired();

            a.Property(x => x.City)
                .HasMaxLength(ConfigurationConstraint.MIN20_TEXT_LENGTH)
                .HasColumnName("city");

            a.Property(x => x.House)
                .HasMaxLength(ConfigurationConstraint.MIN20_TEXT_LENGTH)
                .HasColumnName("house");

            a.Property(x => x.Flat)
                .HasMaxLength(ConfigurationConstraint.MIN20_TEXT_LENGTH)
                .HasColumnName("flat");
        });

        builder.ComplexProperty(x => x.Phone, p =>
        {
            p.Property(x => x.Phone)
                .IsRequired()
                .HasMaxLength(ConfigurationConstraint.MIN20_TEXT_LENGTH)
                .HasColumnName("phone");
        });

        builder.ComplexProperty(x => x.SpeciesDetail, sd =>
        {
            sd.IsRequired();

            sd.Property(x => x.SpeciesId)
                .HasConversion(
                    id => id.Value,
                    value => SpeciesId.Create(value))
                .HasColumnName("species_id");

            sd.Property(x => x.BreedId)
                .HasColumnName("breed_id");
        });

        builder.ComplexProperty(x => x.Position, sn =>
        {
            sn.Property(x => x.Value)
                .IsRequired()
                .HasColumnName("position");
        });

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

        builder.Property(x => x.Requisites)
            .HasConversion(rq => JsonSerializer.Serialize(rq, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<List<Requisite>>(json, JsonSerializerOptions.Default)!,
                new ValueComparer<IReadOnlyList<Requisite>>(
                    (c1, c2) => c1!.SequenceEqual(c2!),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()))
            .HasColumnType("jsonb");

        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_Deleted");
    }
}