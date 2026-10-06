using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStudents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Result",
                table: "Reviews",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "StudentSections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subtitle = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    StoriesSubtitle = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    StoriesTitle = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    StoriesText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSections", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentSections");

            migrationBuilder.DropColumn(
                name: "Result",
                table: "Reviews");
        }
    }
}
