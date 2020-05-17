using GTANetworkAPI;
using ProjectFive.MappingManager.dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
            Task createObjects = new Task(() => createAllObjects(allObjects));
            createObjects.Start();
            createObjects.Wait();

            NAPI.World.SetTime(23, 15, 0);

        }

        [ServerEvent(Event.PlayerConnected)]
        public void LoadMappingClientSide(Player player)
        {
            NAPI.ClientEvent.TriggerClientEvent(player, "loadMapping", NAPI.Util.ToJson(allObjects));
        }


        public void createAllObjects(List<MappingObject> allObjects)
        {
            NAPI.Task.Run(() =>
            {
                foreach (MappingObject mapObject in allObjects)
                {
                    var mappingObject = NAPI.Object.CreateObject(mapObject.ModelHash, mapObject.Location, mapObject.Rotation, dimension: mapObject.Dimension);
                }
            });

        }


    }
}
