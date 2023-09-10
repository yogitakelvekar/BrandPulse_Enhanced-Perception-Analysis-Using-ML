using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TermPulse.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Update2SentimentAnalysisTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "PostSentimentAnalysis",
                newName: "SentimentDataId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SentimentDataId",
                table: "PostSentimentAnalysis",
                newName: "PostId");
        }
    }
}
