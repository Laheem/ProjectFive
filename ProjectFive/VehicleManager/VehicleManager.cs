using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

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
        private void SpawnTargetVehicle(Player player, String vehicleName)
        {
            VehicleHash targetVehicle = NAPI.Util.VehicleNameToModel(vehicleName);
            if(targetVehicle == 0)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "That looks like an invalid car name.");
            }
            Vehicle x = NAPI.Vehicle.CreateVehicle(targetVehicle, player.Position, new float(), 255, 255);
            x.EnginePowerMultiplier = 2;
        }
    }
}
