using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrandPulse.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPlatfromTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlatformDefaultUrl",
                table: "Platform",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PlatformIcon",
                table: "Platform",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlatformDefaultUrl",
                table: "Platform");

            migrationBuilder.DropColumn(
                name: "PlatformIcon",
                table: "Platform");
        }
    }
}
