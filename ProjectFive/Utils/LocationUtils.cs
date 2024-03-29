﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GTANetworkAPI;

namespace ProjectFive.Utils
{
    class LocationUtils : Script
    {
        [Command("loc",GreedyArg = true)]
        public void getPlayerLocation(Player player, String send, String comment = "No comment provided.")
        {
            NAPI.Chat.SendChatMessageToPlayer(player, player.Position.ToString());
    
            if (send != null && string.Equals(send, "send", StringComparison.OrdinalIgnoreCase))
            {
                using System.IO.StreamWriter file =
                    new System.IO.StreamWriter("locations.txt", true);
                file.WriteLine($"{player.Position} - {comment} from {player.Name}");
            }
        }
    }
}
