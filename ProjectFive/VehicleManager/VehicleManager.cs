using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using GTANetworkMethods;
using Player = GTANetworkAPI.Player;

namespace ProjectFive.VehicleManager
{
    class VehicleManager : Script
    {
        [ServerEvent(Event.ResourceStart)]
        public void OnVehicleManagerResourceStart()
        {
            NAPI.Util.ConsoleOutput("[VEHICLE MANAGER] Vehicle Manager has successfully started!");
        }

        [Command("vehicle", Alias = "veh", GreedyArg = true)]
        public void SpawnTargetVehicle(Player player, String vehicleName)
        {
            VehicleHash targetVehicle = NAPI.Util.VehicleNameToModel(vehicleName);
            if(targetVehicle == 0)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "That looks like an invalid car name.");
                return;
            }

            NAPI.Task.Run(() =>
            {
                GTANetworkAPI.Vehicle x = NAPI.Vehicle.CreateVehicle(targetVehicle, player.Position, 0f, 1, 1);
                x.Dimension = player.Dimension;
                x.Locked = false;
                NAPI.Entity.SetEntityPosition(x, player.Position);
                NAPI.Player.SetPlayerIntoVehicle(player, x, 0);
            }, delayTime: 500);
        }

        [Command("getveh")]
        public void getVeh(Player player)
        {
            NAPI.Util.ConsoleOutput(NAPI.Pools.GetAllVehicles().Count.ToString());
            foreach(var veh in NAPI.Pools.GetAllVehicles())
            {
                NAPI.Util.ConsoleOutput($" VEHICLE LOCATION - {veh.Position} IN DIMENSION {veh.Dimension}");
            }
          
        }
    }
}
    
