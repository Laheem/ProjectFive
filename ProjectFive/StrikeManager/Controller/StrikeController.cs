using GTANetworkAPI;
using ProjectFive.AccountManager;
using ProjectFive.AdminManager;
using ProjectFive.StrikeManager.Dto;
using ProjectFive.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFive.StrikeManager.Controller
{
    class StrikeController : Script
    {
    
        public void GiveStrike(Player admin, Player offender, String reason)
        {
            if(AdminUtils.isAdminAndCorrectLevel(admin, 1))
            {
                Account adminAccount = AccountUtils.GetAccount(admin);
                Account offenderAccount = AccountUtils.GetAccount(offender);
                offenderAccount.Strikes.Add(new Strike(reason));
            }
        }
    }
}
