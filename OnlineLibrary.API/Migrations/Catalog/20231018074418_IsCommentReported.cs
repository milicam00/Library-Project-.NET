using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineLibrary.API.Migrations.Catalog
{
    /// <inheritdoc />
    public partial class IsCommentReported : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCommentApproved",
                table: "RentalBooks",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCommentReported",
                table: "RentalBooks",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCommentApproved",
                table: "RentalBooks");

            migrationBuilder.DropColumn(
                name: "IsCommentReported",
                table: "RentalBooks");
        }
    }
}
