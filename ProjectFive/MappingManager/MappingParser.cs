using GTANetworkAPI;
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

        }


    }
}
