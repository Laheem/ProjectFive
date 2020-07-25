using GTANetworkAPI;
using ProjectFive.AdminManager;
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
            if (!player.GetData<bool>(DataKeys.FORCE_RESPAWN_KEY)){
                    player.SetData(DataKeys.RESPAWN_KEY, true);
                    ChatUtils.SendInfoMessage(player, "You can now respawn!");
                } else
                {
                    // An admin has respawned instead of the player doing it themselves. Consume their force respawn key for the next time they die.
                    player.SetData(DataKeys.FORCE_RESPAWN_KEY, false);
                }
            }
            , delayTime: 20000);

        }


        public void RespawnPlayer(Player player)
        {
            // TODO - Find nearest hospital and drop them there.
            if (player.GetData<bool>(DataKeys.RESPAWN_KEY))
            {
                spawnPlayer(player);
            }
            else
            {
                ChatUtils.SendInfoMessage(player, "You're not able to respawn yet or you're not dead.");
            }
        }

        private void spawnPlayer(Player player)
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


        public void AdminRespawn(Player admin, String target)
        {
            if (AdminUtils.isAdminAndCorrectLevel(admin, 1))
            {
                Player player = NAPI.Player.GetPlayerFromName(target);
                if (player != null)
                {
                    spawnPlayer(player);
                    player.SetData(DataKeys.FORCE_RESPAWN_KEY, true);
                }
                else
                {
                    ChatUtils.SendInfoMessage(admin, "This player does not exist.");
                }
            }

        }
    }
}
