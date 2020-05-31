using GTANetworkAPI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectFive.DatabaseManager
{
    class DatabaseSchemaManager : Script
    {
        [ServerEvent(Event.ResourceStart)]
        public void HandleSchemaCreation()
        {
            NAPI.Util.ConsoleOutput("[Database Manager] Database Manager is booting...");

            using (var dbContext = new FiveDBContext())
            {
                dbContext.Database.Migrate();
                NAPI.Util.ConsoleOutput($"There are {dbContext.Accounts.Count()} accounts created in the database!");
            }

            NAPI.Util.ConsoleOutput("[Database Manager] Database Manager has booted succesfully.");

        }
    }
}
