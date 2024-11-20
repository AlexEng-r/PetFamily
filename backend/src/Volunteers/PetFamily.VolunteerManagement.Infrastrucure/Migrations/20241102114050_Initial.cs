using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.VolunteerManagement.Infrastrucure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pet_photos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    path = table.Column<string>(type: "text", nullable: false),
                    hash_code = table.Column<string>(type: "text", nullable: false),
                    bucket = table.Column<string>(type: "text", nullable: false),
                    is_main = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pet_photos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pets",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    volunteer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    nick_name = table.Column<string>(type: "text", nullable: false),
                    animal_type = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    color = table.Column<string>(type: "text", nullable: false),
                    health_information = table.Column<string>(type: "text", nullable: true),
                    weight = table.Column<double>(type: "double precision", nullable: true),
                    height = table.Column<double>(type: "double precision", nullable: true),
                    phone = table.Column<string>(type: "text", nullable: false),
                    is_sterialized = table.Column<bool>(type: "boolean", nullable: false),
                    birth_day_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_vaccinated = table.Column<bool>(type: "boolean", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    requisites = table.Column<string>(type: "jsonb", nullable: false),
                    species_id = table.Column<Guid>(type: "uuid", nullable: false),
                    breed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    position = table.Column<int>(type: "integer", nullable: false),
                    date_created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    city = table.Column<string>(type: "text", nullable: false),
                    flat = table.Column<string>(type: "text", nullable: false),
                    house = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pets", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "volunteers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    experience = table.Column<int>(type: "integer", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: false),
                    social_networks = table.Column<string>(type: "jsonb", nullable: false),
                    requisites = table.Column<string>(type: "jsonb", nullable: false),
                    is_Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    firstname = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    patronymic = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    surname = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_volunteers", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pet_photos");

            migrationBuilder.DropTable(
                name: "pets");

            migrationBuilder.DropTable(
                name: "volunteers");
        }
    }
}
