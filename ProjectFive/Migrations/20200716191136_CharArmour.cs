using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectFive.Migrations
{
    public partial class CharArmour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Armour",
                table: "Characters",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Armour",
                table: "Characters");
        }
    }
}
