using GTANetworkAPI;
using ProjectFive.AccountManager;
using ProjectFive.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFive.CharacterManager.Service
{
    #nullable enable
    class CharacterEntityService
    {
        public Character? GetCharacter(Player player)
        {
            return player.GetData<Character>(DataKeys.CHARACTER_KEY);
        }

        public void SetCurrentCharacter(Player player, Character character)
        {
            player.SetData<Character>(DataKeys.CHARACTER_KEY, character);
            player.Name = character.CharacterName;
        }

        public bool HasSelectedCharacter(Player player)
        {
            return player.HasData(DataKeys.CHARACTER_KEY);
        }

    }
}
