using GTANetworkAPI;
using ProjectFive.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectFive.DmvManager
{
    internal class DmvManager : Script
    {
        private static Vector3 DMV_LOCATION_POINT = new Vector3(-1244.6454, -655.16455, 25.641472);
        private static Vector3 DMV_LOCATION_CHECKPOINT = new Vector3(DMV_LOCATION_POINT.X, DMV_LOCATION_POINT.Y, 24);
        private static VehicleHash DMV_VEHICLE = NAPI.Util.VehicleNameToModel("asbo");
        private List<Vehicle> DmvVehicles = new List<Vehicle>();

        private static List<Vector3> DMV_CARS_POINTS = new List<Vector3>() {
            new Vector3(-1237.8527, -666.3745, 25.294832),
            new Vector3(-1239.6742, -664.3326, 25.294594),
            new Vector3(-1241.871, -661.9355, 25.295277)
        };

        private const String IS_IN_DMV_SPAWN_KEY = "dmvSpawnPlayer";
        private const String IS_IN_DMV_TEST_KEY = "dmvteststarted";
        private const String DMV_CAR_NUMBER_KEY = "dmvcarnumber";
        private const String DMV_START_TIME_KEY = "dmvstarttime";
        private const String PLAYER_DMV_VEHICLE = "vehicledrivendmv";

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

            foreach (var carSpawn in DMV_CARS_POINTS)
            {
                Vehicle dmvVeh = SpawnDmvVehicle(carSpawn);
                dmvVeh.SetData<int>(DMV_CAR_NUMBER_KEY, DmvVehicles.Count);
                DmvVehicles.Add(dmvVeh);
                dmvVeh.SetSharedData(VehicleTypeConstants.VEHICLE_TYPE_KEY, VehicleTypeConstants.DMV_VEHICLE_TYPE_VALUE);
            }
        }

        [ServerEvent(Event.PlayerConnected)]
        public void OnPlayerJoin(Player player)
        {
            player.SetData<bool>(IS_IN_DMV_TEST_KEY, false);
        }

        [ServerEvent(Event.PlayerEnterCheckpoint)]
        public void OnPlayerEnterDmvCheckpoint(Checkpoint checkpoint, Player player)
        {
            if (!checkpoint.IsNull && checkpoint.Exists && checkpoint.GetData<String>(CheckpointConstants.CHECKPOINT_LOCATION) == CheckpointConstants.DMV_SPAWN)
            {
                player.SetData<bool>(IS_IN_DMV_SPAWN_KEY, true);
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

        [ServerEvent(Event.PlayerEnterVehicle)]
        public void OnPlayerEnterVehicleAttempt(Player player, Vehicle vehicle, sbyte seatId)
        {
            if (vehicle.GetSharedData<String>(VehicleTypeConstants.VEHICLE_TYPE_KEY) == VehicleTypeConstants.DMV_VEHICLE_TYPE_VALUE)
            {
                if (player.GetData<bool>(IS_IN_DMV_TEST_KEY))
                {
                    vehicle.EngineStatus = true;
                    startTest(player, vehicle);
                }
                else
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, "You're not doing a driving test.");
                    NAPI.Player.WarpPlayerOutOfVehicle(player);
                    vehicle.EngineStatus = false;
                }
            }
        }


        // TODO - Fix once 1.1 is patched.
        [ServerEvent(Event.VehicleDeath)]
        public void OnVehicleDeath(Vehicle vehicle)
        {
            if (vehicle.GetSharedData<String>(VehicleTypeConstants.VEHICLE_TYPE_KEY) == VehicleTypeConstants.DMV_VEHICLE_TYPE_VALUE)
            {
                vehicle.GetData<int>(DMV_CAR_NUMBER_KEY);
                RespawnDmvVehicle(null, vehicle);

                // TODO - FAIL A TEST IF SOME PLAYER IS DRIVING THIS CAR.

            }
        }

        [Command("test")]
        public void startDmvTest(Player player)
        {
            if (player.GetData<bool>(IS_IN_DMV_TEST_KEY) && player.GetData<bool>(IS_IN_DMV_SPAWN_KEY)){
                NAPI.Chat.SendChatMessageToPlayer(player, "You're already doing a DMV test. Get in one of the cars.");
                return;
            }
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

            if (player.GetData<bool>(IS_IN_DMV_TEST_KEY))
            {
                EndTest(player);
                NAPI.Chat.SendChatMessageToPlayer(player, "You have quit your DMV test.");
                player.SetData(IS_IN_DMV_TEST_KEY, false);
                return;
            }

            NAPI.Chat.SendChatMessageToPlayer(player, "You're not in a DMV test.");
        }

       

        [Command("dmv")]
        public void TeleportToDMV(Player player)
        {
            player.Position = DMV_LOCATION_POINT;
        }

        public void startTest(Player player, Vehicle vehicle)
        {
            vehicle.Repair();
            player.SetData<DateTime?>(DMV_START_TIME_KEY, DateTime.Now);
            player.SetData<Vehicle>(PLAYER_DMV_VEHICLE, vehicle);
            NAPI.Chat.SendChatMessageToPlayer(player, "Your test has started, follow the checkpoints!");
            NAPI.ClientEvent.TriggerClientEvent(player, "dmvFirstCheckpoint");
            // TODO - Fire Client events for first checkpoint here.
        }

        public void EndTest(Player player)
        {
            player.SetData<DateTime?>(DMV_START_TIME_KEY, null);
            Vehicle playerDmvVehicle = player.GetData<Vehicle>(PLAYER_DMV_VEHICLE);
            playerDmvVehicle.GetData<int>(DMV_CAR_NUMBER_KEY);
            RespawnDmvVehicle(player, playerDmvVehicle);
            player.SetData<Vehicle>(PLAYER_DMV_VEHICLE, null);
        }


        public void RespawnDmvVehicle(Player? player, Vehicle vehicle)
        {

            var targetVehicleSpawn = DMV_CARS_POINTS.ElementAt(vehicle.GetData<int>(DMV_CAR_NUMBER_KEY));
            SpawnDmvVehicle(targetVehicleSpawn);
            if(player != null && player.Vehicle == vehicle) {
                NAPI.Player.WarpPlayerOutOfVehicle(player);
            }
            vehicle.Delete();

        }

        private Vehicle SpawnDmvVehicle(Vector3 carLocation)
        {
            Vehicle dmvVeh = NAPI.Vehicle.CreateVehicle(DMV_VEHICLE, carLocation, 310, 255, 255, numberPlate: "DMV CAR", engine: false);
            return dmvVeh;
        }
    }
}