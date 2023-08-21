using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrandPulse.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedMissingColumnInPostDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PostAuthorProfile",
                table: "PostDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostAuthorProfile",
                table: "PostDetail");
        }
    }
}
