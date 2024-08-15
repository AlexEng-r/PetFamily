using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address",
                table: "pets");

            migrationBuilder.RenameColumn(
                name: "full_name_surname",
                table: "volunteers",
                newName: "surname");

            migrationBuilder.RenameColumn(
                name: "full_name_patronymic",
                table: "volunteers",
                newName: "patronymic");

            migrationBuilder.RenameColumn(
                name: "full_name_first_name",
                table: "volunteers",
                newName: "firstname");

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "pets",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "flat",
                table: "pets",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "house",
                table: "pets",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "city",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "flat",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "house",
                table: "pets");

            migrationBuilder.RenameColumn(
                name: "surname",
                table: "volunteers",
                newName: "full_name_surname");

            migrationBuilder.RenameColumn(
                name: "patronymic",
                table: "volunteers",
                newName: "full_name_patronymic");

            migrationBuilder.RenameColumn(
                name: "firstname",
                table: "volunteers",
                newName: "full_name_first_name");

            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "pets",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
