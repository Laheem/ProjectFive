using GTANetworkAPI;
using ProjectFive.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFive.AccountManager
{
    class AccountEntityService
    {

#nullable enable
            public Account? GetAccount(Player player)
            {
                return player.GetData<Account>(DataKeys.ACCOUNT_KEY);
            }

            public void SetCurrentAccount(Player player, Account Account)
            {
                player.SetData<Account>(DataKeys.ACCOUNT_KEY, Account);
            }

            public bool HasSelectedAccount(Player player)
            {
                return player.HasData(DataKeys.ACCOUNT_KEY);
            }
        }
}
