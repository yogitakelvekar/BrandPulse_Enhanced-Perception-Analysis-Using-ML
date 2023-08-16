using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrandPulse.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedWordCloudAnalysisTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SentimentScore",
                table: "PostSentimentData");

            migrationBuilder.CreateTable(
                name: "PostWordCloudAnalysis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SearchTermId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hashtag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostWordCloudAnalysis", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostWordCloudAnalysis");

            migrationBuilder.AddColumn<float>(
                name: "SentimentScore",
                table: "PostSentimentData",
                type: "real",
                nullable: true);
        }
    }
}
