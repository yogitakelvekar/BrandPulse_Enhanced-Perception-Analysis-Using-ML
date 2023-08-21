using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrandPulse.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MajorDBDesignChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlatformId",
                table: "PostWordCloudData");

            migrationBuilder.DropColumn(
                name: "PostDate",
                table: "PostWordCloudData");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "PostWordCloudData");

            migrationBuilder.DropColumn(
                name: "SearchTermId",
                table: "PostWordCloudData");

            migrationBuilder.DropColumn(
                name: "PlatformId",
                table: "PostSentimentData");

            migrationBuilder.DropColumn(
                name: "SearchTermId",
                table: "PostSentimentData");

            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "PostInfluencerData");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "PostInfluencerData");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "PostInfluencerData");

            migrationBuilder.DropColumn(
                name: "PlatformId",
                table: "PostInfluencerData");

            migrationBuilder.DropColumn(
                name: "PostDate",
                table: "PostInfluencerData");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "PostInfluencerData");

            migrationBuilder.DropColumn(
                name: "Profile",
                table: "PostInfluencerData");

            migrationBuilder.DropColumn(
                name: "SearchTermId",
                table: "PostInfluencerData");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "PostSentimentData",
                newName: "SubPostId");

            migrationBuilder.RenameColumn(
                name: "PostDate",
                table: "PostSentimentData",
                newName: "SubPostDate");

            migrationBuilder.AddColumn<Guid>(
                name: "PostDetailId",
                table: "PostWordCloudData",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PostDetailId",
                table: "PostSentimentData",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PostDetailId",
                table: "PostInfluencerData",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "PostDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SearchTermId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlatformId = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostThumbnail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostAuthor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostAuthorAvatar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostDetail", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostDetail");

            migrationBuilder.DropColumn(
                name: "PostDetailId",
                table: "PostWordCloudData");

            migrationBuilder.DropColumn(
                name: "PostDetailId",
                table: "PostSentimentData");

            migrationBuilder.DropColumn(
                name: "PostDetailId",
                table: "PostInfluencerData");

            migrationBuilder.RenameColumn(
                name: "SubPostId",
                table: "PostSentimentData",
                newName: "PostId");

            migrationBuilder.RenameColumn(
                name: "SubPostDate",
                table: "PostSentimentData",
                newName: "PostDate");

            migrationBuilder.AddColumn<int>(
                name: "PlatformId",
                table: "PostWordCloudData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PostDate",
                table: "PostWordCloudData",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PostId",
                table: "PostWordCloudData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SearchTermId",
                table: "PostWordCloudData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PlatformId",
                table: "PostSentimentData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SearchTermId",
                table: "PostSentimentData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "PostInfluencerData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "PostInfluencerData",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "PostInfluencerData",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlatformId",
                table: "PostInfluencerData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PostDate",
                table: "PostInfluencerData",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PostId",
                table: "PostInfluencerData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Profile",
                table: "PostInfluencerData",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SearchTermId",
                table: "PostInfluencerData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
