using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAboutPageKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PageKey",
                table: "Abouts",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "home");

            migrationBuilder.CreateIndex(
                name: "IX_Abouts_PageKey",
                table: "Abouts",
                column: "PageKey",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Abouts_PageKey",
                table: "Abouts");

            migrationBuilder.DropColumn(
                name: "PageKey",
                table: "Abouts");
        }
    }
}
