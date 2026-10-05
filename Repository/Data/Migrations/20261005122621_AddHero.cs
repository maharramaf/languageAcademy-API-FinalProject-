using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddHero : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Heroes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Eyebrow = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    Lead = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PrimaryButtonText = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    SecondaryButtonText = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    PointOne = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    PointTwo = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    PointThree = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(260)", maxLength: 260, nullable: false),
                    ImageAlt = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StudentsStat = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    StudentsLabel = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    RatingStat = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    RatingLabel = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Heroes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Heroes");
        }
    }
}
