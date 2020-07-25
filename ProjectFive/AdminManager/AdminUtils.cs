using GTANetworkAPI;
using ProjectFive.AccountManager;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFive.AdminManager
{
    static class AdminUtils
    {
        static AccountEntityService accountEntityService = new AccountEntityService();

       public static bool isAdminAndCorrectLevel(Player target, int requiredLevel)
        {
            Account playerAccount = accountEntityService.GetAccount(target);
            if(playerAccount.AdminLevel < requiredLevel)
            {
                ProjectFive.Utils.ChatUtils.SendInfoMessage(target, "Not the correct admin level.");
            }
            return playerAccount.AdminLevel >= requiredLevel;
        }


    }
}
