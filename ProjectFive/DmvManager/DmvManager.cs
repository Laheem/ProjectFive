using GTANetworkAPI;
using ProjectFive.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFive.DmvManager
{
    class DmvManager : Script
    {
        private static Vector3 DMV_LOCATION_POINT = new Vector3(-1244.6454, -655.16455 ,25.641472);
        private static Vector3 DMV_LOCATION_CHECKPOINT = new Vector3(DMV_LOCATION_POINT.X, DMV_LOCATION_POINT.Y, 24);
        private const String IS_IN_DMV_SPAWN_KEY = "dmvSpawnPlayer";
        
        [ServerEvent(Event.ResourceStart)]
        public void OnDmvManagerStart()
        {
            var dmvBlip = NAPI.Blip.CreateBlip(DMV_LOCATION_POINT);
            dmvBlip.Sprite = 663;
            dmvBlip.Color = 25;
            dmvBlip.Name = "DMV";

            var dmvCheckpoint = NAPI.Checkpoint.CreateCheckpoint(CheckpointType.Cyclinder, DMV_LOCATION_CHECKPOINT, new Vector3(), 2.5f, new Color(255, 0, 0));
            dmvCheckpoint.SetData<String>(CheckpointConstants.CHECKPOINT_LOCATION, CheckpointConstants.DMV_SPAWN);
            NAPI.Util.ConsoleOutput("[DMV Manager] - DMV Manager ready!");
        }


        [ServerEvent(Event.PlayerEnterCheckpoint)]
        public void OnPlayerEnterDmvCheckpoint(Checkpoint checkpoint, Player player)
        {
            if(!checkpoint.IsNull && checkpoint.Exists && checkpoint.GetData<String>(CheckpointConstants.CHECKPOINT_LOCATION) == CheckpointConstants.DMV_SPAWN)
            {
                player.SetData<bool>(IS_IN_DMV_SPAWN_KEY,true);
            }
        }

        [ServerEvent(Event.PlayerExitCheckpoint)]
        public void OnPlayerExitDmvCheckpoint(Checkpoint checkpoint, Player player)
        {

            if (!checkpoint.IsNull && checkpoint.Exists && checkpoint.GetData<String>(CheckpointConstants.CHECKPOINT_LOCATION) == CheckpointConstants.DMV_SPAWN)
            {
                player.SetData<bool>(IS_IN_DMV_SPAWN_KEY, false);
            }
        }


        [Command("test")]
        public void startDMVTest(Player player)
        {
            NAPI.Chat.SendChatMessageToAll(player.GetData<bool>(IS_IN_DMV_SPAWN_KEY).ToString());
            if (player.GetData<bool>(IS_IN_DMV_SPAWN_KEY))
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "Starting your test. Please get in the car provided.");
                NAPI.Chat.SendChatMessageToPlayer(player, "You will have TIMEHERE to complete the test. Try not to damage the car too much, or you will fail.");
            }
            else
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "You're not at the DMV.");
            }
        }

        [Command("dmv")]
        public void TeleportToDMV(Player player)
        {
            player.Position = DMV_LOCATION_POINT;
        }        
    }
}
