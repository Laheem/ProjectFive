using RAGE;
using RAGE.Elements;
using RAGE.NUI;
using System.Collections.Generic;
using System.Drawing;

namespace ProjectFiveClient.Client.Login
{
    class CharacterSelect : RAGE.Events.Script
    {
        private MenuPool _menuPool;
        public CharacterSelect()
        {
            RAGE.Events.Add("loginSuccess", onCharacterListSent);
            RAGE.Events.Tick += Tick;
            _menuPool = new MenuPool();
        }

        private void onCharacterListSent(object[] args)
        {
            RAGE.Chat.Output("The function works.");
            var bytes = RAGE.Util.MsgPack.ConvertFromJson((string)args[0]);
            List<string> characters = RAGE.Util.MsgPack.Deserialize<List<string>>(bytes);
            Point RIGHT_EDGE_OF_SCREEN = new Point(1400, 50);
            UIMenu mainMenu = new UIMenu("Character Select", "Choose which character you want to play.", RIGHT_EDGE_OF_SCREEN);
            mainMenu.FreezeAllInput = true;
            _menuPool.Add(mainMenu);

            AddCharacterButtons(characters, mainMenu);
            _menuPool.RefreshIndex();
            mainMenu.Visible = true;

        }


        private void AddCharacterButtons(List<string> characters, UIMenu targetMenu)
        {
            foreach (string characterName in characters)
            {
                UIMenuItem characterItem = new UIMenuItem(characterName);
                characterItem.Activated += CharacterButtonActivated;
                targetMenu.AddItem(characterItem);
            }
            if (characters.Count < 4)
            {
                UIMenuItem createACharacterItem = new UIMenuItem("Create a Character");
                targetMenu.AddItem(createACharacterItem);

            }
        }

        private void CharacterButtonActivated(UIMenu sender, UIMenuItem selectedItem)
        {
            sender.Visible = false;
            RAGE.Events.CallRemote("selectCharacter", selectedItem.Text);
            Player.LocalPlayer.FreezePosition(false);
        }

        private void CreateCharacterButtonActivated(UIMenu sender, UIMenuItem selectedItem)
        {
            sender.Visible = false;
            RAGE.Events.CallLocal("createCharacter");
        }

        void Tick(List<Events.TickNametagData> nametags)
        {
            _menuPool.ProcessMenus();
        }

    }
}
