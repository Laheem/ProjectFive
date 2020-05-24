using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectFive.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    SocialClubId = table.Column<ulong>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsAdmin = table.Column<bool>(nullable: false),
                    IsDev = table.Column<bool>(nullable: false),
                    IsVip = table.Column<bool>(nullable: false),
                    IsBanned = table.Column<bool>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    ServerRank = table.Column<string>(nullable: true),
                    VipLevel = table.Column<int>(nullable: false),
                    VipExpiration = table.Column<DateTime>(nullable: false),
                    VipTokens = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.SocialClubId);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    CharacterId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CharacterName = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    Race = table.Column<string>(nullable: true),
                    Job = table.Column<string>(nullable: true),
                    PlayingHours = table.Column<int>(nullable: false),
                    Money = table.Column<long>(nullable: false),
                    AccountSocialClubId = table.Column<ulong>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.CharacterId);
                    table.ForeignKey(
                        name: "FK_Characters_Accounts_AccountSocialClubId",
                        column: x => x.AccountSocialClubId,
                        principalTable: "Accounts",
                        principalColumn: "SocialClubId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_AccountSocialClubId",
                table: "Characters",
                column: "AccountSocialClubId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
