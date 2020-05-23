using System;
using System.Collections.Generic;
using System.Text;
using RAGE;
using RAGE.Game;

namespace ProjectFiveClient.Client.Mapping
{
    class ClientMapping : Events.Script
    {
        List<int> allEntityCounts = new List<int>();

        public ClientMapping()
        {
            Events.OnPlayerWeaponShot += Something;
            Events.OnPlayerQuit += RemoveObjects;
            Events.Add("mappingJson", LoadMapping);
        }

        private void RemoveObjects(RAGE.Elements.Player player)
        {
            for(int x = 0; x < allEntityCounts.Count; x++)
            {
                var targetNumber = allEntityCounts[x];
                RAGE.Game.Object.DeleteObject(ref targetNumber);
                allEntityCounts[x] = -1;
            }
        }

        private void Something(Vector3 targetPos, RAGE.Elements.Player target, Events.CancelEventArgs cancel)
        {
            Chat.Output("Bang bang");
        }

        public void LoadMapping(object[] args)
        {
            RAGE.Chat.Output("Welcome to the server!");
            var settings = new Newtonsoft.Json.JsonSerializerSettings();
            var allObjects = RAGE.Util.Json.Deserialize<List<MappingObject>>((string)args[0],settings);
            foreach (MappingObject mappingObject in allObjects)
            {
                int objectEntity = RAGE.Game.Object.CreateObject(mappingObject.ModelHash, mappingObject.XLoc, mappingObject.YLoc, mappingObject.ZLoc, true, false, true);
                RAGE.Game.Entity.FreezeEntityPosition(objectEntity, true);
                allEntityCounts.Add(objectEntity);
                RAGE.Game.Entity.SetEntityRotation(objectEntity, mappingObject.XRot, mappingObject.YRot, mappingObject.ZRot, 3, true);
                RAGE.Game.Invoker.Invoke(0x58A850EAEE20FAA3, objectEntity);

                
            }
            
        }
    }
}
