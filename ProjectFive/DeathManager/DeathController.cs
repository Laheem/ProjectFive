using GTANetworkAPI;
using ProjectFive.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFive.DeathManager
{
    class DeathController : Script
    {
        [ServerEvent(Event.ResourceStart)]
        public void DisableDefaultDeathHandler()
        {
            NAPI.Util.ConsoleOutput("[DEATH MANAGER] - Death Manager has successfully booted. Ominious.");
            NAPI.Server.SetAutoRespawnAfterDeath(false);
        }

        [ServerEvent(Event.PlayerDeath)]
        public void OnPlayerDeath(Player player, Player killer, uint reason)
        {
            NAPI.Player.PlayPlayerAnimation(player, 0, "dead", "dead_d");
            ChatUtils.SendInfoMessage(player, "You have died. Wait 20 seconds to respawn. You must interact with any RP with your body before respawning.", "~r~");

            NAPI.Task.Run(() =>
            {
                player.SetData(DataKeys.RESPAWN_KEY, true);
                ChatUtils.SendInfoMessage(player, "You can now respawn!");
            }
            , delayTime: 20000);

        }


        public void RespawnPlayer(Player player)
        {
            // TODO - Find nearest hospital and drop them there.
            if (player.Dead)
            {
                if (player.GetData<bool>(DataKeys.RESPAWN_KEY))
                {
                    NAPI.ClientEvent.TriggerClientEvent(player, "blackout");
                    NAPI.Task.Run(() =>
                    {
                        NAPI.ClientEvent.TriggerClientEvent(player, "deathFadeIn");
                        NAPI.Player.SpawnPlayer(player, new Vector3(0, 50, 0));
                        player.SetData<bool>(DataKeys.RESPAWN_KEY, false);
                    }
                    , delayTime: 7000);
                  }
                else
                {
                    ChatUtils.SendInfoMessage(player, "You're not able to respawn yet.");
                }
            } else
            {
                ChatUtils.SendInfoMessage(player, "You're not dead.");
            }
        }
    }
}
