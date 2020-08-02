using GTANetworkAPI;
using ProjectFive.CharacterManager;
using ProjectFive.CharacterManager.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFive.Utils
{
    public static class CharacterUtils
    {
        static CharacterEntityService characterEntityService = new CharacterEntityService();

        public static Character? GetCharacter(Player player)
        {
            return characterEntityService.GetCharacter(player);
        }

    }
}
