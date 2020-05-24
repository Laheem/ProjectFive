using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectFive.Migrations
{
    public partial class characterFKUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Accounts_AccountSocialClubId",
                table: "Characters");

            migrationBuilder.AlterColumn<ulong>(
                name: "AccountSocialClubId",
                table: "Characters",
                nullable: false,
                oldClrType: typeof(ulong),
                oldType: "bigint unsigned",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Accounts_AccountSocialClubId",
                table: "Characters",
                column: "AccountSocialClubId",
                principalTable: "Accounts",
                principalColumn: "SocialClubId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Accounts_AccountSocialClubId",
                table: "Characters");

            migrationBuilder.AlterColumn<ulong>(
                name: "AccountSocialClubId",
                table: "Characters",
                type: "bigint unsigned",
                nullable: true,
                oldClrType: typeof(ulong));

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Accounts_AccountSocialClubId",
                table: "Characters",
                column: "AccountSocialClubId",
                principalTable: "Accounts",
                principalColumn: "SocialClubId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
