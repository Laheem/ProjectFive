using MessagePack;
using RAGE;
using RAGE.Game;
using RAGE.NUI;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Drawing;

namespace WeaponsManager
{
    class WeaponsUI : RAGE.Events.Script
    {
        private MenuPool _menuPool;

        public WeaponsUI()
        {
            _menuPool = new MenuPool();
            RAGE.Events.Add("weaponList", generateWeaponsUI);
            RAGE.Events.Tick += Tick;



            void generateWeaponsUI(object[] args)
            {
                _menuPool = new MenuPool();
                UIMenu mainMenu = new UIMenu("Weapons List", "Select a type of weapon you want", new Point(900,50));

                UIMenu meleeMenu = _menuPool.AddSubMenu(mainMenu, "Melee", new Point(900,50));
                UIMenu pistolMenu = _menuPool.AddSubMenu(mainMenu, "Pistols", new Point(900, 50));
                UIMenu smgMenu = _menuPool.AddSubMenu(mainMenu, "Sub Machine Guns", new Point(900, 50));
                UIMenu shotgunMenu = _menuPool.AddSubMenu(mainMenu, "Shotguns", new Point(900, 50));
                UIMenu rifleMenu = _menuPool.AddSubMenu(mainMenu, "Rifles", new Point(900, 50));
                UIMenu heavyMenu = _menuPool.AddSubMenu(mainMenu, "Heavy Weapons", new Point(900, 50));
                UIMenu sniperMenu = _menuPool.AddSubMenu(mainMenu, "Sniper Rifles", new Point(900, 50));
                UIMenu throwableMenu = _menuPool.AddSubMenu(mainMenu, "Throwables", new Point(900, 50));

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

   

        void AddWeaponButtons(List<String> listOfWeapons, UIMenu targetMenu)
        {
            foreach(String weapon in listOfWeapons)
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
