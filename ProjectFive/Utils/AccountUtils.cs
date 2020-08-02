using GTANetworkAPI;
using ProjectFive.AccountManager;
using ProjectFive.CharacterManager;
using ProjectFive.CharacterManager.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFive.Utils
{
    public static class AccountUtils
    {
        static AccountEntityService accountEntityService = new AccountEntityService();

        public static Account? GetAccount(Player player)
        {
            return accountEntityService.GetAccount(player);
        }

    }
}
