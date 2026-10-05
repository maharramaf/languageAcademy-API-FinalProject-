using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAbout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abouts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Eyebrow = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    ParagraphOne = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ParagraphTwo = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    FeatureOne = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    FeatureTwo = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    FeatureThree = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    FeatureFour = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    ButtonText = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(260)", maxLength: 260, nullable: false),
                    ImageAlt = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    YearsStat = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    YearsLabelLineOne = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    YearsLabelLineTwo = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abouts", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abouts");
        }
    }
}
