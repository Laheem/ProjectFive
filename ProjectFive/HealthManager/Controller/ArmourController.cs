using GTANetworkAPI;
using ProjectFive.AccountManager;
using ProjectFive.CharacterManager;
using ProjectFive.CharacterManager.Service;
using ProjectFive.HealthManager.Dto;
using ProjectFive.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFive.HealthManager.Service
{
    class ArmourController : Script
    {

        CharacterEntityService characterEntityService = new CharacterEntityService();
        AccountEntityService accountEntityService = new AccountEntityService();

        public void SetPlayerArmour(Player player, ArmourType armourType)
        {
            Character targetChar = characterEntityService.GetCharacter(player);
            if (CanStackArmourType(armourType, targetChar.Armour)){
                SetCharacterArmour(player, (int)armourType);
            } else
            {
                ChatUtils.SendInfoMessage(player, "Your armour quality is too low to fix up your current armour.");
            }
        }

        public void AdminSetPlayerArmour(Player player, String targetPlayerName, String armour)
        {
          
            Account adminAccount = accountEntityService.GetAccount(player);
            if(adminAccount.AdminLevel >= 1)
            {
                if (!int.TryParse(armour, out int armourAmount) && armourAmount > -1)
                {
                    ChatUtils.SendInfoMessage(player, "Invalid armour amount. Try a number from 0-100");
                }
                Player targetPlayer = NAPI.Player.GetPlayerFromName(targetPlayerName);
                if (targetPlayer == null)
                {
                    ChatUtils.SendInfoMessage(player, "That player doesn't exist!");
                    return;
                }
                SetCharacterArmour(targetPlayer, armourAmount);
            }
        }


        private bool CanStackArmourType(ArmourType armourType, int currentArmour)
        {
            return (int)armourType > currentArmour;
        }

        private void SetCharacterArmour(Player player, int armourAmount)
        {
            if(armourAmount > 100)
            {
                armourAmount = 100;
            }
            Character targetChar = characterEntityService.GetCharacter(player);
            targetChar.Armour = armourAmount;
            player.Armor = armourAmount;
        }
    }
}
