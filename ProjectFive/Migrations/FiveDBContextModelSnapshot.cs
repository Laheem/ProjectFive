﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectFive.DatabaseManager;

namespace ProjectFive.Migrations
{
    [DbContext(typeof(FiveDBContext))]
    partial class FiveDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ProjectFive.AccountManager.Account", b =>
                {
                    b.Property<ulong>("SocialClubId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<int>("AdminLevel")
                        .HasColumnType("int");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsBanned")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDev")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsHeadDev")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsHeadNarrative")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsVip")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Password")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ServerRank")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("SocialClubName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("StrikeLevel")
                        .HasColumnType("int");

                    b.Property<DateTime?>("VipExpiration")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("VipLevel")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("VipTokens")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("SocialClubId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("ProjectFive.AccountManager.Dto.Strike", b =>
                {
                    b.Property<int>("StrikeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<ulong?>("AccountSocialClubId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("AdminName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("ExpireTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("HasExpired")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Reason")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("StrikeId");

                    b.HasIndex("AccountSocialClubId");

                    b.ToTable("Strikes");
                });

            modelBuilder.Entity("ProjectFive.CharacterManager.Character", b =>
                {
                    b.Property<int>("CharacterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<ulong>("AccountSocialClubId")
                        .HasColumnType("bigint unsigned");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int>("Armour")
                        .HasColumnType("int");

                    b.Property<string>("CharacterName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Gender")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Health")
                        .HasColumnType("int");

                    b.Property<string>("Job")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("Money")
                        .HasColumnType("bigint");

                    b.Property<int>("PlayingHours")
                        .HasColumnType("int");

                    b.Property<float>("PositionX")
                        .HasColumnType("float");

                    b.Property<float>("PositionY")
                        .HasColumnType("float");

                    b.Property<float>("PositionZ")
                        .HasColumnType("float");

                    b.Property<string>("Race")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("CharacterId");

                    b.HasIndex("AccountSocialClubId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("ProjectFive.AccountManager.Dto.Strike", b =>
                {
                    b.HasOne("ProjectFive.AccountManager.Account", null)
                        .WithMany("Strikes")
                        .HasForeignKey("AccountSocialClubId");
                });

            modelBuilder.Entity("ProjectFive.CharacterManager.Character", b =>
                {
                    b.HasOne("ProjectFive.AccountManager.Account", null)
                        .WithMany("Characters")
                        .HasForeignKey("AccountSocialClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
