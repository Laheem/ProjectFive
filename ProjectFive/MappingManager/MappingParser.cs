using GTANetworkAPI;
using ProjectFive.MappingManager.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFive.MappingManager
{
    class MappingParser : Script
    {
        [ServerEvent(Event.ResourceStart)]
        public void LoadMapping()
        {
            NAPI.Util.ConsoleOutput("[MAPPING MANAGER] Now parsing all available mapping... This may take a while.");
            List<MappingObject> allObjects = ServerParser.getAllMappingObjects();
            foreach (MappingObject mapObject in allObjects)
            {
                NAPI.Object.CreateObject(mapObject.ModelHash, mapObject.Location, mapObject.Rotation, dimension: mapObject.Dimension);
                Console.WriteLine("OBJECT CREATED!");
            }

        }


    }
}
