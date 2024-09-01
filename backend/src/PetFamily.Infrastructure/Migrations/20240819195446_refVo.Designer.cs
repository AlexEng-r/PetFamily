﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PetFamily.Infrastructure.DatabaseContext;

#nullable disable

namespace PetFamily.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240819195446_refVo")]
    partial class refVo
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PetFamily.Domain.Breeds.Breed", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid?>("species_id")
                        .HasColumnType("uuid")
                        .HasColumnName("species_id");

                    b.ComplexProperty<Dictionary<string, object>>("Name", "PetFamily.Domain.Breeds.Breed.Name#NotEmptyString", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)")
                                .HasColumnName("name");
                        });

                    b.HasKey("Id")
                        .HasName("pk_breeds");

                    b.HasIndex("species_id")
                        .HasDatabaseName("ix_breeds_species_id");

                    b.ToTable("breeds", (string)null);
                });

            modelBuilder.Entity("PetFamily.Domain.PetPhotos.PetPhoto", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("IsMain")
                        .HasColumnType("boolean")
                        .HasColumnName("is_main");

                    b.Property<Guid?>("pet_id")
                        .HasColumnType("uuid")
                        .HasColumnName("pet_id");

                    b.ComplexProperty<Dictionary<string, object>>("Path", "PetFamily.Domain.PetPhotos.PetPhoto.Path#NotEmptyString", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("path");
                        });

                    b.HasKey("Id")
                        .HasName("pk_pet_photos");

                    b.HasIndex("pet_id")
                        .HasDatabaseName("ix_pet_photos_pet_id");

                    b.ToTable("pet_photos", (string)null);
                });

            modelBuilder.Entity("PetFamily.Domain.Pets.Pet", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime?>("BirthDayDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("birth_day_date");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_created");

                    b.Property<double?>("Height")
                        .HasColumnType("double precision")
                        .HasColumnName("height");

                    b.Property<bool>("IsSterialized")
                        .HasColumnType("boolean")
                        .HasColumnName("is_sterialized");

                    b.Property<bool>("IsVaccinated")
                        .HasColumnType("boolean")
                        .HasColumnName("is_vaccinated");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("status");

                    b.Property<double?>("Weight")
                        .HasColumnType("double precision")
                        .HasColumnName("weight");

                    b.Property<Guid?>("volunteer_id")
                        .HasColumnType("uuid")
                        .HasColumnName("volunteer_id");

                    b.ComplexProperty<Dictionary<string, object>>("Address", "PetFamily.Domain.Pets.Pet.Address#Address", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)")
                                .HasColumnName("city");

                            b1.Property<string>("Flat")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)")
                                .HasColumnName("flat");

                            b1.Property<string>("House")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)")
                                .HasColumnName("house");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("AnimalType", "PetFamily.Domain.Pets.Pet.AnimalType#NotEmptyString", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("animal_type");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Breed", "PetFamily.Domain.Pets.Pet.Breed#CanBeEmptyString", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("breed");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Color", "PetFamily.Domain.Pets.Pet.Color#NotEmptyString", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)")
                                .HasColumnName("color");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Description", "PetFamily.Domain.Pets.Pet.Description#CanBeEmptyString", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .HasMaxLength(2000)
                                .HasColumnType("character varying(2000)")
                                .HasColumnName("description");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("HealthInformation", "PetFamily.Domain.Pets.Pet.HealthInformation#CanBeEmptyString", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("health_information");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("NickName", "PetFamily.Domain.Pets.Pet.NickName#NotEmptyString", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)")
                                .HasColumnName("nick_name");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Phone", "PetFamily.Domain.Pets.Pet.Phone#ContactPhone", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)")
                                .HasColumnName("phone");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("SpeciesDetail", "PetFamily.Domain.Pets.Pet.SpeciesDetail#SpeciesDetail", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<Guid>("BreedId")
                                .HasColumnType("uuid")
                                .HasColumnName("breed_id");

                            b1.Property<Guid>("SpeciesId")
                                .HasColumnType("uuid")
                                .HasColumnName("species_id");
                        });

                    b.HasKey("Id")
                        .HasName("pk_pets");

                    b.HasIndex("volunteer_id")
                        .HasDatabaseName("ix_pets_volunteer_id");

                    b.ToTable("pets", (string)null);
                });

            modelBuilder.Entity("PetFamily.Domain.Specieses.Species", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.ComplexProperty<Dictionary<string, object>>("Name", "PetFamily.Domain.Specieses.Species.Name#NotEmptyString", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)")
                                .HasColumnName("name");
                        });

                    b.HasKey("Id")
                        .HasName("pk_species");

                    b.ToTable("species", (string)null);
                });

            modelBuilder.Entity("PetFamily.Domain.Volunteers.Volunteer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("Experience")
                        .HasColumnType("integer")
                        .HasColumnName("experience");

                    b.ComplexProperty<Dictionary<string, object>>("Description", "PetFamily.Domain.Volunteers.Volunteer.Description#NotEmptyString", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(2000)
                                .HasColumnType("character varying(2000)")
                                .HasColumnName("description");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("FullName", "PetFamily.Domain.Volunteers.Volunteer.FullName#FullName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)")
                                .HasColumnName("firstname");

                            b1.Property<string>("Patronymic")
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)")
                                .HasColumnName("patronymic");

                            b1.Property<string>("Surname")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)")
                                .HasColumnName("surname");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Phone", "PetFamily.Domain.Volunteers.Volunteer.Phone#ContactPhone", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)")
                                .HasColumnName("phone");
                        });

                    b.HasKey("Id")
                        .HasName("pk_volunteers");

                    b.ToTable("volunteers", (string)null);
                });

            modelBuilder.Entity("PetFamily.Domain.Breeds.Breed", b =>
                {
                    b.HasOne("PetFamily.Domain.Specieses.Species", null)
                        .WithMany("Breeds")
                        .HasForeignKey("species_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .HasConstraintName("fk_breeds_species_species_id");
                });

            modelBuilder.Entity("PetFamily.Domain.PetPhotos.PetPhoto", b =>
                {
                    b.HasOne("PetFamily.Domain.Pets.Pet", null)
                        .WithMany("PetPhotos")
                        .HasForeignKey("pet_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_pet_photos_pets_pet_id");
                });

            modelBuilder.Entity("PetFamily.Domain.Pets.Pet", b =>
                {
                    b.HasOne("PetFamily.Domain.Volunteers.Volunteer", null)
                        .WithMany("Pets")
                        .HasForeignKey("volunteer_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_pets_volunteers_volunteer_id");

                    b.OwnsOne("PetFamily.Domain.Requisites.RequisiteDetails", "Requisites", b1 =>
                        {
                            b1.Property<Guid>("PetId")
                                .HasColumnType("uuid");

                            b1.HasKey("PetId")
                                .HasName("pk_pets");

                            b1.ToTable("pets");

                            b1.ToJson("Requisites");

                            b1.WithOwner()
                                .HasForeignKey("PetId")
                                .HasConstraintName("fk_pets_pets_pet_id");

                            b1.OwnsMany("PetFamily.Domain.Requisites.Requisite", "Requisites", b2 =>
                                {
                                    b2.Property<Guid>("RequisiteDetailsPetId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Description")
                                        .IsRequired()
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasMaxLength(20)
                                        .HasColumnType("character varying(20)");

                                    b2.HasKey("RequisiteDetailsPetId", "Id")
                                        .HasName("pk_pets");

                                    b2.ToTable("pets");

                                    b2.WithOwner()
                                        .HasForeignKey("RequisiteDetailsPetId")
                                        .HasConstraintName("fk_pets_pets_requisite_details_pet_id");
                                });

                            b1.Navigation("Requisites");
                        });

                    b.Navigation("Requisites")
                        .IsRequired();
                });

            modelBuilder.Entity("PetFamily.Domain.Volunteers.Volunteer", b =>
                {
                    b.OwnsOne("PetFamily.Domain.SocialNetworks.SocialNetworkDetails", "SocialNetworks", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("volunteers");

                            b1.ToJson("SocialNetworks");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volunteers_volunteers_id");

                            b1.OwnsMany("PetFamily.Domain.SocialNetworks.SocialNetwork", "SocialNetworks", b2 =>
                                {
                                    b2.Property<Guid>("SocialNetworkDetailsVolunteerId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Link")
                                        .IsRequired()
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasMaxLength(20)
                                        .HasColumnType("character varying(20)");

                                    b2.HasKey("SocialNetworkDetailsVolunteerId", "Id")
                                        .HasName("pk_volunteers");

                                    b2.ToTable("volunteers");

                                    b2.WithOwner()
                                        .HasForeignKey("SocialNetworkDetailsVolunteerId")
                                        .HasConstraintName("fk_volunteers_volunteers_social_network_details_volunteer_id");
                                });

                            b1.Navigation("SocialNetworks");
                        });

                    b.OwnsOne("PetFamily.Domain.Requisites.RequisiteDetails", "Requisites", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("volunteers");

                            b1.ToJson("Requisites");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volunteers_volunteers_id");

                            b1.OwnsMany("PetFamily.Domain.Requisites.Requisite", "Requisites", b2 =>
                                {
                                    b2.Property<Guid>("RequisiteDetailsVolunteerId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Description")
                                        .IsRequired()
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasMaxLength(20)
                                        .HasColumnType("character varying(20)");

                                    b2.HasKey("RequisiteDetailsVolunteerId", "Id")
                                        .HasName("pk_volunteers");

                                    b2.ToTable("volunteers");

                                    b2.WithOwner()
                                        .HasForeignKey("RequisiteDetailsVolunteerId")
                                        .HasConstraintName("fk_volunteers_volunteers_requisite_details_volunteer_id");
                                });

                            b1.Navigation("Requisites");
                        });

                    b.Navigation("Requisites")
                        .IsRequired();

                    b.Navigation("SocialNetworks")
                        .IsRequired();
                });

            modelBuilder.Entity("PetFamily.Domain.Pets.Pet", b =>
                {
                    b.Navigation("PetPhotos");
                });

            modelBuilder.Entity("PetFamily.Domain.Specieses.Species", b =>
                {
                    b.Navigation("Breeds");
                });

            modelBuilder.Entity("PetFamily.Domain.Volunteers.Volunteer", b =>
                {
                    b.Navigation("Pets");
                });
#pragma warning restore 612, 618
        }
    }
}
