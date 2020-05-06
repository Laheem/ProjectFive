using System;
using GTANetworkAPI;

namespace ProjectFive
{
    public class Core : Script
    {
        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStart()
        {
            NAPI.Util.ConsoleOutput("[CORE] Server booted up succesfully...");
        }
    }
}
