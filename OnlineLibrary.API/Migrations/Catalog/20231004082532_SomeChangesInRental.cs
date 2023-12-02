using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineLibrary.API.Migrations.Catalog
{
    /// <inheritdoc />
    public partial class SomeChangesInRental : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentalDate",
                table: "RentalBooks");

            migrationBuilder.DropColumn(
                name: "ReturnDate",
                table: "RentalBooks");

            migrationBuilder.DropColumn(
                name: "Returned",
                table: "RentalBooks");

            migrationBuilder.AddColumn<DateTime>(
                name: "RentalDate",
                table: "Rentals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnDate",
                table: "Rentals",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Returned",
                table: "Rentals",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentalDate",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "ReturnDate",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "Returned",
                table: "Rentals");

            migrationBuilder.AddColumn<DateTime>(
                name: "RentalDate",
                table: "RentalBooks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnDate",
                table: "RentalBooks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Returned",
                table: "RentalBooks",
                type: "bit",
                nullable: true);
        }
    }
}
