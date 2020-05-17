using System;
using System.Collections.Generic;
using System.Text;
using ProjectFive.MappingManager.dto;
using RAGE;

namespace ProjectFiveClient.Client.MappingManager
{
    class MappingLoader : RAGE.Events.Script
    {

        public MappingLoader()
        {
            RAGE.Events.Add("loadMapping", CreateMapping);
        }


        void CreateMapping(object[] args)
        {
            //RAGE.Chat.Output("Event fired..");
            //var bytes = RAGE.Util.Json.Deserialize<List<MappingObject>>((string)args[0], );
            //List<MappingObject> mappingObjects = RAGE.Util.MsgPack.Deserialize<List<MappingObject>>(bytes);

            //if(mappingObjects == null)
            //{
            //    RAGE.Chat.Output("oh no brader");
            //}
            //RAGE.Chat.Output(mappingObjects.Count.ToString());
            //foreach (MappingObject mapObject in mappingObjects)
            //{
            //    RAGE.Game.Object.CreateObject((uint)mapObject.ModelHash, mapObject.Location.X, mapObject.Location.Y, mapObject.Location.Z, true, false, true);
            //}
        }
    }
}
