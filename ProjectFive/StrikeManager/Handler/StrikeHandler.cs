using GTANetworkAPI;
using ProjectFive.StrikeManager.Controller;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFive.StrikeManager
{
    class StrikeHandler : Script
    {
        StrikeController strikeController = new StrikeController();

        [Command("strikes",GreedyArg = true)]
        public void CheckStrikes(Player player, String targetPlayer)
        {

        }


        [Command("hstrikes", GreedyArg = true)]
        public void CheckHistoricStrikes(Player player, String targetPlayer)
        {

        }

        [Command("warn", GreedyArg = true)]
        public void CheckCurrentStrikes(Player player, String targetPlayer)
        {

        }

        [Command("voidstrike", GreedyArg = true)]
        public void VoidStrike(Player player, String targetPlayer)
        {

        }

        [Command("deletestrike", GreedyArg = true)]
        public void DeleteStrike(Player player, String targetPlayer)
        {

        }


    }
}
