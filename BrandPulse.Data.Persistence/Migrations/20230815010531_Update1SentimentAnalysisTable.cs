using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrandPulse.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Update1SentimentAnalysisTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SearchTermId",
                table: "PostSentimentAnalysis",
                newName: "PostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "PostSentimentAnalysis",
                newName: "SearchTermId");
        }
    }
}
