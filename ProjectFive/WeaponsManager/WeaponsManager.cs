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

        [Command("giveweapon", Alias = "givewep")]
        public void giveWeapon(Player player, String weaponName )
        {
            WeaponHash targetWeapon = NAPI.Util.WeaponNameToModel(weaponName);
            if(targetWeapon == 0)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "That's an invalid weapon name.");
            } else
            {
                NAPI.Player.GivePlayerWeapon(player, targetWeapon, 9999);
            }
        }


    }
}
