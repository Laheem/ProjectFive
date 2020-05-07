using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using GTANetworkAPI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        public void GiveWeapon(Player player, String weaponName )
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

        [Command("weapon", Alias = "wep")]
        public void CreateWeaponList(Player player)
        {
            NAPI.ClientEvent.TriggerClientEvent(player, "weaponList");
        }

        [RemoteEvent("giveWeapon")]
        public void HandleWeaponSelectionEvent(Player player, object[] arguments)
        {
            String weaponName = (String) arguments[0];
            GiveWeapon(player, weaponName);
        }

    }
}
