using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(260)", maxLength: 260, nullable: false),
                    PhotoAlt = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Home = table.Column<bool>(type: "bit", nullable: false),
                    Approved = table.Column<bool>(type: "bit", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.CheckConstraint("CK_Reviews_Rating", "[Rating] >= 1 AND [Rating] <= 5");
                    table.ForeignKey(
                        name: "FK_Reviews_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReviewSections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subtitle = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewSections", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_Approved_Home_Order",
                table: "Reviews",
                columns: new[] { "Approved", "Home", "Order" });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CourseId",
                table: "Reviews",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "ReviewSections");
        }
    }
}
