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
        CharacterHandler characterHandler = new CharacterHandler();
        
        public CharacterSaveService()
        {
            CharacterHandler characterHandler = new CharacterHandler();
            characterHandler.CharacterLoggedIn += CharacterHandler_CharacterLoggedIn;
        }

        private void CharacterHandler_CharacterLoggedIn(object sender, EventArgs e)
        {
            var characterLogInArgs = (CharacterLogInArgs)e;
            var targetPlayer = characterLogInArgs.client;
            var targetCharacter = characterLogInArgs.targetCharacter;

            targetPlayer.Armor = targetCharacter.Armour;

        }

        [ServerEvent(Event.PlayerDisconnected)]
        public void SaveCharacterData(Player player, DisconnectionType type, String reason)
        {
            var playerChar = characterEntityService.GetCharacter(player);
            if (playerChar != null)
            {
                playerChar.Armour = player.Armor;
                characterService.SaveCharacter(playerChar);
            }
        }


       
    }
}
