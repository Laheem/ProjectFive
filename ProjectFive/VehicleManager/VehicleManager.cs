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
                Vehicle x = NAPI.Vehicle.CreateVehicle(targetVehicle, player.Position, new float(), 255, 255);
                x.Dimension = player.Dimension;
                x.Locked = false;
                NAPI.Entity.SetEntityPosition(x, player.Position);
                NAPI.Player.SetPlayerIntoVehicle(player, x, 0);
               
            });
        }

        [Command("getveh")]
        public void getVeh(Player player)
        {
            NAPI.Util.ConsoleOutput(NAPI.Pools.GetAllVehicles().Count.ToString());
        }
    }

}
    
