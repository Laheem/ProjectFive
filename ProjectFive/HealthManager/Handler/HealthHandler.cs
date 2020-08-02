using GTANetworkAPI;
using ProjectFive.AdminManager;
using ProjectFive.HealthManager.Service;
using ProjectFive.Migrations;
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

        [Command("sethealth")]
        public void AdminHealCommand(Player admin, String playerName, string amount)
        {
            healthController.AdminHeal(admin, playerName, int.Parse(amount));
        }
    }
}
