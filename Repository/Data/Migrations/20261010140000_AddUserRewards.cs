using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Repository.Data;

#nullable disable

namespace Repository.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20261010140000_AddUserRewards")]
    public partial class AddUserRewards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(name: "RewardHomework", table: "AspNetUsers", type: "int", nullable: false, defaultValue: 0);
            migrationBuilder.AddColumn<DateTime>(name: "RewardLastActive", table: "AspNetUsers", type: "datetime2", nullable: true);
            migrationBuilder.AddColumn<int>(name: "RewardLessons", table: "AspNetUsers", type: "int", nullable: false, defaultValue: 0);
            migrationBuilder.AddColumn<int>(name: "RewardPoints", table: "AspNetUsers", type: "int", nullable: false, defaultValue: 0);
            migrationBuilder.AddColumn<int>(name: "RewardQuizzes", table: "AspNetUsers", type: "int", nullable: false, defaultValue: 0);
            migrationBuilder.AddColumn<int>(name: "RewardStreakDays", table: "AspNetUsers", type: "int", nullable: false, defaultValue: 0);
            migrationBuilder.AddColumn<int>(name: "RewardXp", table: "AspNetUsers", type: "int", nullable: false, defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "RewardHomework", table: "AspNetUsers");
            migrationBuilder.DropColumn(name: "RewardLastActive", table: "AspNetUsers");
            migrationBuilder.DropColumn(name: "RewardLessons", table: "AspNetUsers");
            migrationBuilder.DropColumn(name: "RewardPoints", table: "AspNetUsers");
            migrationBuilder.DropColumn(name: "RewardQuizzes", table: "AspNetUsers");
            migrationBuilder.DropColumn(name: "RewardStreakDays", table: "AspNetUsers");
            migrationBuilder.DropColumn(name: "RewardXp", table: "AspNetUsers");
        }
    }
}
