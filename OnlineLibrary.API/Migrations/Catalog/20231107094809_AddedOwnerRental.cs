using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineLibrary.API.Migrations.Catalog
{
    /// <inheritdoc />
    public partial class AddedOwnerRental : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "OutboxMessages",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "OwnerRentals",
                columns: table => new
                {
                    OwnerRentalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RentalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerRentals", x => x.OwnerRentalId);
                    table.ForeignKey(
                        name: "FK_OwnerRentals_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "OwnerId");
                });

            migrationBuilder.CreateTable(
                name: "OwnerRentalBooks",
                columns: table => new
                {
                    OwnerRentalBookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerRentalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerRentalBooks", x => x.OwnerRentalBookId);
                    table.ForeignKey(
                        name: "FK_OwnerRentalBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId");
                    table.ForeignKey(
                        name: "FK_OwnerRentalBooks_OwnerRentals_OwnerRentalId",
                        column: x => x.OwnerRentalId,
                        principalTable: "OwnerRentals",
                        principalColumn: "OwnerRentalId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OwnerRentalBooks_BookId",
                table: "OwnerRentalBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerRentalBooks_OwnerRentalId",
                table: "OwnerRentalBooks",
                column: "OwnerRentalId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerRentals_OwnerId",
                table: "OwnerRentals",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OwnerRentalBooks");

            migrationBuilder.DropTable(
                name: "OwnerRentals");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "OutboxMessages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);
        }
    }
}
