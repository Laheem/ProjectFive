using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFive.DeathManager
{
    class DeathHandler : Script
    {
        DeathController deathController = new DeathController();

        [Command("respawn")]
        public void respawnCommand(Player player)
        {
            deathController.RespawnPlayer(player);
        }

    }
}
