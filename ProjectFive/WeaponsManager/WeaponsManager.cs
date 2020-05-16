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
        public void OnWeaponsManagerResourceStart()
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
                NAPI.Player.GivePlayerWeapon(player, targetWeapon, 999);
            }
        }


        [Command("weapon", Alias = "wep")]
        public void CreateWeaponList(Player player)
        {

            NAPI.ClientEvent.TriggerClientEvent(player, "weaponList",
                GetTargetWeaponList("melee"),
                GetTargetWeaponList("handguns"),
                GetTargetWeaponList("smg"),
                GetTargetWeaponList("shotguns"),
                GetTargetWeaponList("assault_rifles"),
                GetTargetWeaponList("sniper_rifles"),
                GetTargetWeaponList("heavy_weapons"),
                GetTargetWeaponList("throwables")
                );
        }


        [RemoteEvent("giveWeapon")]
        public void HandleWeaponSelectionEvent(Player player, object[] arguments)
        {
            String weaponName = (String) arguments[0];
            GiveWeapon(player, weaponName);
        }




        String GetTargetWeaponList(string weaponName)
        {
            List<String> allWeaponNames = new List<String>();
            using (StreamReader r = new StreamReader("weapons.json"))
            {
                string json = r.ReadToEnd();
                var values = JObject.Parse(json);
                var selectedList = values[weaponName];

                foreach (JProperty item in selectedList)
                {
                    allWeaponNames.Add(item.Name);
                }

                return NAPI.Util.ToJson(allWeaponNames);
            }

        }


    }
}
