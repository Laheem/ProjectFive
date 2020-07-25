using System.Collections.Generic;
using System.Text;

namespace ProjectFiveClient.Client.Login
{
    class CreateCharacter : RAGE.Events.Script
    {
        CreateCharacter()
        {
            RAGE.Events.Add("createCharacter", createCharacterActivated);
        }

        private void createCharacterActivated(object[] args)
        {
            RAGE.Chat.Output("This is where Create a Character goes!");
        }
    }
}
