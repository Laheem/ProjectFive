using RAGE.Elements;
using RAGE.Ui;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

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
            String charName = args[0].ToString() + "_" + args[1].ToString();
            if (ValidateCharacterName(charName) && ValidateCharacterAge(args[3].ToString())){
                RAGE.Events.CallRemote("characterSelectValidate", charName, args[2].ToString(), args[3].ToString());
                destroyCreatorBrowser();
            } else
            {
                RAGE.Chat.Output("Some fields are invalid.");
            }
        }

        private void createCharacterActivated(object[] args)
        {
            CreateCharacterSelectBrowser();
        }

        public void CreateCharacterSelectBrowser()
        {
            browser = new HtmlWindow(CREATE_A_CHARACTER_PATH);
            browser.Active = true;
            Cursor.Visible = true;
            RAGE.Chat.Activate(false);
            Player.LocalPlayer.FreezePosition(true);
            RAGE.Game.Player.SetPlayerControl(true, 0);
        }


        private bool ValidateCharacterName(String characterName)
        {
            Regex regex = new Regex("^[A-Z]{1}[a-z]{1,}_[A-Z]{1}[a-z]{1,}$");
            return regex.IsMatch(characterName);
        }

        private bool ValidateCharacterAge(String age)
        {
            int.TryParse(age, out int numericAge);
            return numericAge >= 18 && numericAge <= 65;
        }

        public void destroyCreatorBrowser()
        {
            if (browser != null)
            {
                RAGE.Ui.Cursor.Visible = false;
                browser.Destroy();
                RAGE.Chat.Activate(true);
                Player.LocalPlayer.FreezePosition(false);
            }
        }


    }
}
