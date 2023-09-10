using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TermPulse.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedPlatformTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PostSearchDetails",
                table: "PostSearchDetails");

            migrationBuilder.RenameTable(
                name: "PostSearchDetails",
                newName: "PostSearchDetail");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostSearchDetail",
                table: "PostSearchDetail",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Platform",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platform", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Platform");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostSearchDetail",
                table: "PostSearchDetail");

            migrationBuilder.RenameTable(
                name: "PostSearchDetail",
                newName: "PostSearchDetails");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostSearchDetails",
                table: "PostSearchDetails",
                column: "Id");
        }
    }
}
