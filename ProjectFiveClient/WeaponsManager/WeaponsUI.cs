using Newtonsoft.Json.Linq;
using RAGE.Game;
using RAGE.NUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace ProjectFiveClient.WeaponsManager
{
    class WeaponsUI : RAGE.Events.Script
    {
        public WeaponsUI()
        {
            MenuPool _menuPool; 
            RAGE.Events.Add("weaponList", generateWeaponsUI);

            void generateWeaponsUI(object[] args)
            {
                _menuPool = new MenuPool();
                UIMenu mainMenu = new UIMenu("Weapons List", "Select a type of weapon you want");

                _menuPool.Add(mainMenu);

                UIMenu meleeMenu = _menuPool.AddSubMenu(mainMenu, "Melee");
                UIMenu pistolMenu = _menuPool.AddSubMenu(mainMenu, "Pistols");
                UIMenu smgMenu = _menuPool.AddSubMenu(mainMenu, "Sub Machine Guns");
                UIMenu shotgunMenu = _menuPool.AddSubMenu(mainMenu, "Shotguns");
                UIMenu rifleMenu = _menuPool.AddSubMenu(mainMenu, "Rifles");
                UIMenu heavyMenu = _menuPool.AddSubMenu(mainMenu, "Heavy Weapons");
                UIMenu sniperMenu = _menuPool.AddSubMenu(mainMenu, "Sniper Rifles");
                UIMenu throwableMenu = _menuPool.AddSubMenu(mainMenu, "Throwables");


                AddWeaponButtons("melee", meleeMenu);
                AddWeaponButtons("pistol", pistolMenu);
                AddWeaponButtons("smg", smgMenu);
                AddWeaponButtons("shotguns", shotgunMenu);
                AddWeaponButtons("assault_rifles", rifleMenu);
                AddWeaponButtons("sniper_rifles", rifleMenu);
                AddWeaponButtons("heavy_weapons", heavyMenu);
                AddWeaponButtons("throwables", throwableMenu);

                mainMenu.Visible = true;
            }
        }

        List<String> GetTargetWeaponList(string weaponName)
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

                return allWeaponNames;
            }

        }

        void AddWeaponButtons(String weaponType, UIMenu targetMenu)
        {
            List<String> listOfWeapons = GetTargetWeaponList(weaponType);
            foreach(String weapon in listOfWeapons)
            {
                UIMenuItem weaponItem = new UIMenuItem(weapon);
                weaponItem.Activated += WeaponItem_Activated; 
            }
        }

        private void WeaponItem_Activated(UIMenu sender, UIMenuItem selectedItem)
        {
            RAGE.Events.CallRemote("giveWeapon", selectedItem.Text);
        }
    }

        
}
