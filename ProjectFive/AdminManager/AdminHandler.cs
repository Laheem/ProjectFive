using GTANetworkAPI;
using ProjectFive.AccountManager;
using ProjectFive.DatabaseManager;
using ProjectFive.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFive.AdminManager
{
    class AdminHandler : Script
    {
        AccountEntityService accountEntityService = new AccountEntityService();
        AdminController adminController = new AdminController();

        [Command("setadmin")]
        public void SetAdminLevelCommand(Player player, String playerName, int levelToSet)
        {
            if (accountEntityService.HasSelectedAccount(player) && adminController.isValidAdminLevel(levelToSet))
            {
                Account playerAccount = accountEntityService.GetAccount(player);

                if (playerAccount.AdminLevel >= 4 && levelToSet <= playerAccount.AdminLevel)
                {
                    Player targetAdmin = NAPI.Player.GetPlayerFromName(playerName);

                    if (targetAdmin != null && accountEntityService.HasSelectedAccount(targetAdmin))
                    {
                        adminController.SetAdminLevel(accountEntityService.GetAccount(player), levelToSet);
                        NAPI.Chat.SendChatMessageToPlayer(player, $"Succesfully set {playerName} to a Level {levelToSet} admin.");
                    }
                    else
                    {
                        NAPI.Chat.SendChatMessageToPlayer(player, $"{playerName} isn't recognised or they aren't signed in.");
                    }
                }
                else
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, "You're not the right admin level to do that. You need to be a level 4.");
                }
            }
            else
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "Not logged in or the admin level is invalid.");
            }
        }


        [Command("admin")]
        public void DebugAdminCommand(Player player, int adminLevel)
        {
            if (accountEntityService.HasSelectedAccount(player) && adminController.isValidAdminLevel(adminLevel))
            {
                adminController.SetAdminLevel(accountEntityService.GetAccount(player), adminLevel);
                NAPI.Chat.SendChatMessageToPlayer(player, $"Done! You're now a level {adminLevel} admin.");
            }
            else
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "Admin level appears to be invalid or you're not logged in.");
            }
        }

        [Command("getadmin")]
        public void GetAdminLevel(Player player)
        {
            if (accountEntityService.HasSelectedAccount(player))
            {
                NAPI.Chat.SendChatMessageToPlayer(player, $"Your current admin level is - {accountEntityService.GetAccount(player).AdminLevel}");
            }
            else
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "Log in.");
            }
        }


        [Command("removeadmin")]
        public void RemoveAdminCommand(Player player)
        {
            if (accountEntityService.HasSelectedAccount(player))
            {

                NAPI.Chat.SendChatMessageToPlayer(player, "Removed your admin level.");
                adminController.SetAdminLevel(accountEntityService.GetAccount(player), 0);
            }
            else
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "Log in!");
            }
        }
    }
}
