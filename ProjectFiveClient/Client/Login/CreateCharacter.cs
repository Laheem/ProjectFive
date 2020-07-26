using RAGE.Elements;
using RAGE.Ui;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFiveClient.Client.Login
{
    class CreateCharacter : RAGE.Events.Script
    {
        HtmlWindow browser;
        private const string CREATE_A_CHARACTER_PATH = "package://cef/html/createcharacter.html";
        CreateCharacter()
        {
            RAGE.Events.Add("createCharacter", createCharacterActivated);
            RAGE.Events.Add("characterSubmit", validateCharacter);
        }

        private void validateCharacter(object[] args)
        {
            throw new NotImplementedException();
        }

        private void createCharacterActivated(object[] args)
        {
            RAGE.Chat.Output("This is where Create a Character goes!");
            createLoginBrowser();
        }

        public void createLoginBrowser()
        {
            browser = new HtmlWindow(CREATE_A_CHARACTER_PATH);
            browser.Active = true;
            Cursor.Visible = true;
            RAGE.Chat.Activate(false);
            Player.LocalPlayer.FreezePosition(true);
            RAGE.Game.Player.SetPlayerControl(true, 0);
        }

    }
}
