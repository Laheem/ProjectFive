using GTANetworkAPI;
using ProjectFive.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ProjectFive.DmvManager
{
    class DmvManager : Script
    {
        private static Vector3 DMV_LOCATION_POINT = new Vector3(-1244.6454, -655.16455, 25.641472);
        private static Vector3 DMV_LOCATION_CHECKPOINT = new Vector3(DMV_LOCATION_POINT.X, DMV_LOCATION_POINT.Y, 24);
        private static VehicleHash DMV_VEHICLE = NAPI.Util.VehicleNameToModel("asbo");

        private static List<Vector3> DMV_CARS_POINTS = new List<Vector3>() {
            new Vector3(-1237.8527, -666.3745, 25.294832),
            new Vector3(-1239.6742, -664.3326, 25.294594),
            new Vector3(-1241.871, -661.9355, 25.295277)
        };


        private const String IS_IN_DMV_SPAWN_KEY = "dmvSpawnPlayer";
        private const String IS_IN_DMV_TEST_KEY = "dmvteststarted";
        
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

            foreach(var carSpawn in DMV_CARS_POINTS)
            {
                
                NAPI.Vehicle.CreateVehicle(DMV_VEHICLE, carSpawn, 310, 255, 255, numberPlate: "DMV CAR", engine: false);
            }


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
        public void startDmvTest(Player player)
        {
            NAPI.Chat.SendChatMessageToAll(player.GetData<bool>(IS_IN_DMV_SPAWN_KEY).ToString());
            if (player.GetData<bool>(IS_IN_DMV_SPAWN_KEY))
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "Starting your test. Please get in the car provided.");
                NAPI.Chat.SendChatMessageToPlayer(player, "You will have TIMEHERE to complete the test. Try not to damage the car too much, or you will fail.");
                player.SetData<bool>(IS_IN_DMV_TEST_KEY, true);
            }
            else
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "You're not at the DMV.");
            }
        }

        [Command("quittest")]
        public void EndDmvTest(Player player)
        {
            // TODO - Despawn DMV car if needed.
            player.SetData<bool>(IS_IN_DMV_TEST_KEY, false);
        }

        [Command("dmv")]
        public void TeleportToDMV(Player player)
        {
            player.Position = DMV_LOCATION_POINT;
        }        
    }
}
