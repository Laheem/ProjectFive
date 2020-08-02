using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectFive.Migrations
{
    public partial class strikes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StrikeLevel",
                table: "Accounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Strikes",
                columns: table => new
                {
                    StrikeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Reason = table.Column<string>(nullable: true),
                    AdminName = table.Column<string>(nullable: true),
                    HasExpired = table.Column<bool>(nullable: false),
                    ExpireTime = table.Column<DateTime>(nullable: false),
                    AccountSocialClubId = table.Column<ulong>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Strikes", x => x.StrikeId);
                    table.ForeignKey(
                        name: "FK_Strikes_Accounts_AccountSocialClubId",
                        column: x => x.AccountSocialClubId,
                        principalTable: "Accounts",
                        principalColumn: "SocialClubId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Strikes_AccountSocialClubId",
                table: "Strikes",
                column: "AccountSocialClubId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Strikes");

            migrationBuilder.DropColumn(
                name: "StrikeLevel",
                table: "Accounts");
        }
    }
}
