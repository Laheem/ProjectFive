using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFive.CharacterManager
{
    class CharacterHandler : Script
    {
        CharacterController characterController = new CharacterController();

        [Command("stats")]
        public void GetCharacterStats(Player player)
        {
            characterController.GetCharacterStats(player);
        }


    }
}
