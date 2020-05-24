using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectFive.Migrations
{
    public partial class testUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "VipExpiration",
                table: "Accounts",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddColumn<string>(
                name: "test",
                table: "Accounts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "test",
                table: "Accounts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "VipExpiration",
                table: "Accounts",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
