using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectFive.Migrations
{
    public partial class SocialClubName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "test",
                table: "Accounts");

            migrationBuilder.AddColumn<string>(
                name: "SocialClubName",
                table: "Accounts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SocialClubName",
                table: "Accounts");

            migrationBuilder.AddColumn<string>(
                name: "test",
                table: "Accounts",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
