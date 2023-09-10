using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TermPulse.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Update4SentimentAnalysisTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sentiment",
                table: "PostSentimentAnalysis",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sentiment",
                table: "PostSentimentAnalysis");
        }
    }
}
