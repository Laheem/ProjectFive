using Microsoft.EntityFrameworkCore;
using ProjectFive.AccountManager;
using System;
using System.Collections.Generic;
using System.Text;
using Pomelo.EntityFrameworkCore.MySql;
using System.Runtime.InteropServices;
using System.IO;
using Microsoft.Extensions.Options;
using ProjectFive.CharacterManager;
using ProjectFive.StrikeManager.Dto;

namespace ProjectFive.DatabaseManager
{
    class FiveDBContext : DbContext
    {
        private const string devConnectionString = "Server=localhost;Database=projectfive;Uid=root;Pwd=toor";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                optionsBuilder.UseMySql(devConnectionString);
            } else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                // TODO - CHANGE THIS ONCE YOU HAVE VAULT UP AND RUNNING.

                String liveConnectionString = File.ReadAllText("constr");
                optionsBuilder.UseMySql(liveConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(acc => acc.VipLevel)
                .HasDefaultValue(0);
            modelBuilder.Entity<Account>()
                .Property(acc => acc.VipTokens)
                .HasDefaultValue(0);
        }


        public DbSet<Account> Accounts { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Strike> Strikes { get; set; }
    }
}
