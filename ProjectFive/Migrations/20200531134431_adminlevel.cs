using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectFive.Migrations
{
    public partial class adminlevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdminLevel",
                table: "Accounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsHeadDev",
                table: "Accounts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHeadNarrative",
                table: "Accounts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminLevel",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "IsHeadDev",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "IsHeadNarrative",
                table: "Accounts");
        }
    }
}
