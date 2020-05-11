using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFive.Utils
{
    class ChatUtils : Script
    {
        public static void SendChatMessageToPlayersInRange(Player player, String message, double distance = 50, string colour = "~w~")
        {
            List<Player> allPlayersInRadius = NAPI.Player.GetPlayersInRadiusOfPlayer(distance, player);

            foreach(var targetPlayer in allPlayersInRadius)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, colour + message);
            }
        }

    }
}
