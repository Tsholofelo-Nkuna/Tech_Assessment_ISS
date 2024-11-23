using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Question3.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class FixIsPrimaryContactFlag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrimaryContact",
                table: "Clients");

            migrationBuilder.AddColumn<bool>(
                name: "IsPrimaryContact",
                table: "Contacts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrimaryContact",
                table: "Contacts");

            migrationBuilder.AddColumn<bool>(
                name: "IsPrimaryContact",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
