using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Repository.Data;

#nullable disable

namespace Repository.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20261009170000_AddUserTeacherPlan")]
    public partial class AddUserTeacherPlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeacherPlan",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeacherPlan",
                table: "AspNetUsers");
        }
    }
}
