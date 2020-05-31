using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTANetworkAPI;
using ProjectFive.DatabaseManager;
using BCrypt;
using BCrypt.Net;
using ProjectFive.Utils;

namespace ProjectFive.AccountManager
{
    class AccountsHandler : Script
    {
        readonly AccountService accountService = new AccountService();

        [Command("signup")]
        public void CreateAccount(Player player, String password)
        {
            Account newAccount = new Account {
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                SocialClubId = player.SocialClubId,
                SocialClubName = player.SocialClubName
            };

            switch (accountService.CreateAccount(newAccount))
            {
                case CreateDatabaseStatus.AccountCreated:
                    NAPI.Chat.SendChatMessageToPlayer(player, "Your account was succesfully created.");
                    break;
                case CreateDatabaseStatus.AccountAlreadyExists:
                    NAPI.Chat.SendChatMessageToPlayer(player, "You already have an account here!");
                    break;
                default:
                    NAPI.Chat.SendChatMessageToPlayer(player, "An unknown error occured...");
                    break;
            }
        }

        [Command("login",SensitiveInfo = true)]
        public void LoginUser(Player player, String password)
        {
            Account playerAccount = accountService.LoginAccount(player.SocialClubId, password, out LoginDatabaseStatus status);

            switch (status)
            {
                case LoginDatabaseStatus.AccountDoesntExist:
                    NAPI.Chat.SendChatMessageToPlayer(player, "That account doesn't exist.");
                    break;
                case LoginDatabaseStatus.Success:
                    NAPI.Chat.SendChatMessageToPlayer(player, $"Account logged in, welcome back {playerAccount.SocialClubName}!");
                    player.SetData<Account>(DataKeys.ACCOUNT_KEY, playerAccount);
                    break;
                case LoginDatabaseStatus.IncorrectPassword:
                    NAPI.Chat.SendChatMessageToPlayer(player, "That password is incorrect.");
                    break;
                case LoginDatabaseStatus.UnknownError:
                    NAPI.Chat.SendChatMessageToPlayer(player, "An unknown error occured.");
                    break;
            }
        }
    }
}
