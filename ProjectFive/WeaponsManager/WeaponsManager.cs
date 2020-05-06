using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using GTANetworkAPI;

namespace ProjectFive.WeaponsManager
{
    class WeaponsManager : Script
    {
        [ServerEvent(Event.ResourceStart)]
        public void onWeaponsManagerResourceStart()
        {
            NAPI.Util.ConsoleOutput("[WEAPONS MANAGER] Weapons Manager has booted...");
        } 


    }
}
