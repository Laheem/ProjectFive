using RAGE;
using RAGE.NUI;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace WeaponsManager
{
    internal class WeaponsUI : RAGE.Events.Script
    {
        private MenuPool _menuPool;

        public WeaponsUI()
        {
            _menuPool = new MenuPool();
            RAGE.Events.Add("weaponList", generateWeaponsUI);
            RAGE.Events.Tick += Tick;

            void generateWeaponsUI(object[] args)
            {
                Point RIGHT_EDGE_OF_SCREEN = new Point(1400, 50);
                _menuPool = new MenuPool();
                UIMenu mainMenu = new UIMenu("Weapons List", "Select a type of weapon you want", RIGHT_EDGE_OF_SCREEN);
                mainMenu.FreezeAllInput = true;

                UIMenu meleeMenu = _menuPool.AddSubMenu(mainMenu, "Melee", RIGHT_EDGE_OF_SCREEN);
                UIMenu pistolMenu = _menuPool.AddSubMenu(mainMenu, "Pistols", RIGHT_EDGE_OF_SCREEN);
                UIMenu smgMenu = _menuPool.AddSubMenu(mainMenu, "Sub Machine Guns", RIGHT_EDGE_OF_SCREEN);
                UIMenu shotgunMenu = _menuPool.AddSubMenu(mainMenu, "Shotguns", RIGHT_EDGE_OF_SCREEN);
                UIMenu rifleMenu = _menuPool.AddSubMenu(mainMenu, "Rifles", RIGHT_EDGE_OF_SCREEN);
                UIMenu heavyMenu = _menuPool.AddSubMenu(mainMenu, "Heavy Weapons", RIGHT_EDGE_OF_SCREEN);
                UIMenu sniperMenu = _menuPool.AddSubMenu(mainMenu, "Sniper Rifles", RIGHT_EDGE_OF_SCREEN);
                UIMenu throwableMenu = _menuPool.AddSubMenu(mainMenu, "Throwables", RIGHT_EDGE_OF_SCREEN);

                _menuPool.Add(mainMenu);

                var bytes = RAGE.Util.MsgPack.ConvertFromJson((string)args[0]);
                List<String> weps = RAGE.Util.MsgPack.Deserialize<List<String>>(bytes);

                AddWeaponButtons(ConvertJsonToList((string)args[0]), meleeMenu);
                AddWeaponButtons(ConvertJsonToList((string)args[1]), pistolMenu);
                AddWeaponButtons(ConvertJsonToList((string)args[2]), smgMenu);
                AddWeaponButtons(ConvertJsonToList((string)args[3]), shotgunMenu);
                AddWeaponButtons(ConvertJsonToList((string)args[4]), rifleMenu);
                AddWeaponButtons(ConvertJsonToList((string)args[5]), sniperMenu);
                AddWeaponButtons(ConvertJsonToList((string)args[6]), heavyMenu);
                AddWeaponButtons(ConvertJsonToList((string)args[7]), throwableMenu);

                _menuPool.RefreshIndex();
                mainMenu.Visible = true;
            }

            void Tick(List<Events.TickNametagData> nametags)
            {
                _menuPool.ProcessMenus();
            }
        }

        private void AddWeaponButtons(List<String> listOfWeapons, UIMenu targetMenu)
        {
            foreach (String weapon in listOfWeapons)
            {
                UIMenuItem weaponItem = new UIMenuItem(weapon);
                weaponItem.Activated += WeaponItem_Activated;
                targetMenu.AddItem(weaponItem);
            }
        }

        private void WeaponItem_Activated(UIMenu sender, UIMenuItem selectedItem)
        {
            RAGE.Events.CallRemote("giveWeapon", selectedItem.Text);
        }

        private List<String> ConvertJsonToList(String json)
        {
            var bytes = RAGE.Util.MsgPack.ConvertFromJson(json);
            return RAGE.Util.MsgPack.Deserialize<List<String>>(bytes);
        }
    }
}