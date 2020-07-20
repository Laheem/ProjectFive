using GTANetworkAPI;
using ProjectFive.HealthManager.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFive.HealthManager
{
    class HealthHandler : Script
    {
        HealthController healthController = new HealthController();

        // TODO - Remove amount paid here, mocking purposes
        [Command("heal")]
        public void HealCommand(Player player, String amountPaid)
        {
            healthController.HospitalHeal(player, int.Parse(amountPaid));
        }
    }
}
