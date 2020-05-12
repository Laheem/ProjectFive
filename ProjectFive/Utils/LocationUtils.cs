using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GTANetworkAPI;

namespace ProjectFive.Utils
{
    class LocationUtils : Script
    {
        [Command("loc")]
        public void getPlayerLocation(Player player, String send)
        {
            NAPI.Chat.SendChatMessageToPlayer(player, player.Position.ToString());
            
            if(send != null && send.ToLower() == "send")
            {
                using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter("locations.txt", true))
                {
                    file.WriteLine(player.Position.ToString() + "\n");
                }
            }

        }
    }
}
