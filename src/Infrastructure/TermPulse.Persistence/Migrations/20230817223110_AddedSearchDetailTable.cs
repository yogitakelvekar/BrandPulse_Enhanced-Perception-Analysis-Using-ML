using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TermPulse.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedSearchDetailTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostSearchDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SearchTermId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SearchTerm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SearchDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostSearchDetails", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostSearchDetails");
        }
    }
}
