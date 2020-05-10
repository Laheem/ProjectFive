using Microsoft.EntityFrameworkCore;
using ProjectFive.AccountManager;
using System;
using System.Collections.Generic;
using System.Text;
using Pomelo.EntityFrameworkCore.MySql;

namespace ProjectFive.DatabaseManager
{
    class FiveDBContext : DbContext
    {
        private const string connectionString = "Server=localhost;Database=projectfive;Uid=root;Pwd=toor";

        // Initialize a new MySQL connection with the given connection parameters 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString);
        }


        // Account model class created somewhere else 
        public DbSet<Account> Accounts { get; set; }

    }
}
