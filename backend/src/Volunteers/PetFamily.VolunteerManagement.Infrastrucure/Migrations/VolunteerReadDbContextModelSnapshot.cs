﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PetFamily.VolunteerManagement.Infrastrucure.DatabaseContexts;

#nullable disable

namespace PetFamily.VolunteerManagement.Infrastrucure.Migrations
{
    [DbContext(typeof(VolunteerReadDbContext))]
    partial class VolunteerReadDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PetFamily.VolunteerManagement.Application.Dtos.PetDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("AnimalType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("animal_type");

                    b.Property<DateTime?>("BirthDayDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("birth_day_date");

                    b.Property<Guid>("BreedId")
                        .HasColumnType("uuid")
                        .HasColumnName("breed_id");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("color");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_created");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("HealthInformation")
                        .HasColumnType("text")
                        .HasColumnName("health_information");

                    b.Property<double?>("Height")
                        .HasColumnType("double precision")
                        .HasColumnName("height");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_Deleted");

                    b.Property<bool>("IsSterialized")
                        .HasColumnType("boolean")
                        .HasColumnName("is_sterialized");

                    b.Property<bool>("IsVaccinated")
                        .HasColumnType("boolean")
                        .HasColumnName("is_vaccinated");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nick_name");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone");

                    b.Property<int>("Position")
                        .HasColumnType("integer")
                        .HasColumnName("position");

                    b.Property<string>("Requisites")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("requisites");

                    b.Property<Guid>("SpeciesId")
                        .HasColumnType("uuid")
                        .HasColumnName("species_id");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.Property<Guid>("VolunteerId")
                        .HasColumnType("uuid")
                        .HasColumnName("volunteer_id");

                    b.Property<double?>("Weight")
                        .HasColumnType("double precision")
                        .HasColumnName("weight");

                    b.ComplexProperty<Dictionary<string, object>>("Address", "PetFamily.VolunteerManagement.Application.Dtos.PetDto.Address#AddressDto", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("city");

                            b1.Property<string>("Flat")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("flat");

                            b1.Property<string>("House")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("house");
                        });

                    b.HasKey("Id")
                        .HasName("pk_pets");

                    b.ToTable("pets", (string)null);
                });

            modelBuilder.Entity("PetFamily.VolunteerManagement.Application.Dtos.PetPhotoDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Bucket")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("bucket");

                    b.Property<string>("HashCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("hash_code");

                    b.Property<bool>("IsMain")
                        .HasColumnType("boolean")
                        .HasColumnName("is_main");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("path");

                    b.HasKey("Id")
                        .HasName("pk_pet_photos");

                    b.ToTable("pet_photos", (string)null);
                });

            modelBuilder.Entity("PetFamily.VolunteerManagement.Application.Dtos.VolunteerDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("Experience")
                        .HasColumnType("integer")
                        .HasColumnName("experience");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_Deleted");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone");

                    b.Property<string>("Requisites")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("requisites");

                    b.Property<string>("SocialNetworks")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("social_networks");

                    b.ComplexProperty<Dictionary<string, object>>("FullName", "PetFamily.VolunteerManagement.Application.Dtos.VolunteerDto.FullName#FullNameDto", b1 =>
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

                    b.HasKey("Id")
                        .HasName("pk_volunteers");

                    b.ToTable("volunteers", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
