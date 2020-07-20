using GTANetworkAPI;
using ProjectFive.CharacterManager.Dto;
using ProjectFive.DatabaseManager.Service;
using ProjectFive.Migrations;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;

namespace ProjectFive.CharacterManager.Service
{
    class CharacterSaveService : Script
    {
        CharacterEntityService characterEntityService = new CharacterEntityService();
        CharacterService characterService = new CharacterService();

        public CharacterSaveService()
        {
            CharacterHandler.CharacterLoggedIn += CharacterHandler_CharacterLoggedIn;
           
        }


        public void CharacterHandler_CharacterLoggedIn(object sender, EventArgs e)
        {
            var characterLogInArgs = (CharacterLogInArgs)e;
            var targetPlayer = characterLogInArgs.client;
            var targetCharacter = characterLogInArgs.targetCharacter;

            targetPlayer.Armor = targetCharacter.Armour;
            targetPlayer.Health = targetCharacter.Health;

        }

        [ServerEvent(Event.PlayerDisconnected)]
        public void SaveCharacterData(Player player, DisconnectionType type, String reason)
        {
            var playerChar = characterEntityService.GetCharacter(player);
            if (playerChar != null)
            {
                playerChar.Armour = player.Armor;
                playerChar.Health = player.Health;
                characterService.SaveCharacter(playerChar);
            }
        }


       
    }
}
