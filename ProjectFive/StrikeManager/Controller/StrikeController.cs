using GTANetworkAPI;
using ProjectFive.AccountManager;
using ProjectFive.AdminManager;
using ProjectFive.CharacterManager;
using ProjectFive.DatabaseManager;
using ProjectFive.DatabaseManager.Service;
using ProjectFive.StrikeManager.Dto;
using ProjectFive.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFive.StrikeManager.Controller
{
    class StrikeController : Script
    {
        StrikeService strikeService = new StrikeService();
        CharacterService characterService = new CharacterService();
        AccountService accountService = new AccountService();
        

        public void GiveStrikeOnlinePlayer(Player admin, Player offender, String reason)
        {
            if(AdminUtils.isAdminAndCorrectLevel(admin, 1))
            {
                Account adminAccount = AccountUtils.GetAccount(admin);
                Account offenderAccount = AccountUtils.GetAccount(offender);
                Character character = CharacterUtils.GetCharacter(offender);
                StrikePlayer(reason, adminAccount, offenderAccount, true);
                ChatUtils.SendInfoMessage(admin, $"{character.CharacterName} has received a strike for {reason}.");
                // TODO LOGS HERE
                ChatUtils.SendInfoMessage(offender, $"You have received a strike for {reason} from ${adminAccount.AdminName}. Further rulebreaking will result in punishment, review the rules.");
            }
        }

        // TODO - include field in Strike to ensure player reads it when they log on.
        private void StrikePlayer(string reason, Account adminAccount, Account offenderAccount, bool acknowledged)
        {
            strikeService.CreateStrike(new Strike
            {
                Reason = reason,
                AccountSocialClubId = offenderAccount.SocialClubId,
                StrikingAdminName = adminAccount.AdminName,
                OffenderAcknowledged = acknowledged
            }) ;
        }

        public void GiveStrikeOffline(Player admin, String characterName, String reason)
        {
            if(AdminUtils.isAdminAndCorrectLevel(admin, 1))
            {
                Account adminAccount = AccountUtils.GetAccount(admin);
                Character offendingCharacter = characterService.GetCharacter(characterName);
                if (offendingCharacter != null)
                {
                    Account offendingAccount = accountService.GetAccount(offendingCharacter.AccountSocialClubId);
                    StrikePlayer(reason, adminAccount, offendingAccount, false);
                    ChatUtils.SendInfoMessage(admin, $"${offendingCharacter.CharacterName} was offline. Warning has been applied and they will be notified on next login.");
                }
                ChatUtils.SendInfoMessage(admin, "Couldn't find a character with that name, try again.");
            }
        }
    }
}
