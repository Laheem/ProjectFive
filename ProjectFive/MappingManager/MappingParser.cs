using GTANetworkAPI;
using ProjectFive.MappingManager.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFive.MappingManager
{
    class MappingParser : Script
    {
        List<MappingObject> allObjects = new List<MappingObject>();
        [ServerEvent(Event.ResourceStart)]
        public void LoadMapping()
        {
            NAPI.Util.ConsoleOutput("[MAPPING MANAGER] Now parsing all available mapping... This may take a while.");
            allObjects = ServerParser.getAllMappingObjects();
            NAPI.Task.Run(() =>
            {
                foreach (MappingObject mapObject in allObjects)
                {
                    var mappingObject = NAPI.Object.CreateObject(mapObject.ModelHash, mapObject.Location, mapObject.Rotation, dimension: mapObject.Dimension);
                    Console.WriteLine("OBJECT CREATED!");
                }
            });

        }

        [ServerEvent(Event.PlayerConnected)]
        public void LoadMappingClientSide(Player player)
        {
            NAPI.ClientEvent.TriggerClientEvent(player, "loadMapping", NAPI.Util.ToJson(allObjects));
        }


    }
}
