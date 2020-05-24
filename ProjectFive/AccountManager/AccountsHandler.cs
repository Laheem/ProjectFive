using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTANetworkAPI;
using ProjectFive.DatabaseManager;
using BCrypt;
using BCrypt.Net;

namespace ProjectFive.AccountManager
{
    class AccountsHandler : Script
    {
        readonly AccountService accountService = new AccountService();

        [Command("signup")]
        public void CreateAccount(Player player, String password)
        {
          var status = accountService.CreateAccount(player.SocialClubId, password);
          if(status == CreateDatabaseStatus.AccountCreated)
            {
                NAPI.Chat.SendChatMessageToPlayer(player,"Your account was succesfully created.");
            } else
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "An unknown error occured...");
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
                    NAPI.Chat.SendChatMessageToPlayer(player, $"Account logged in, welcome back {playerAccount.SocialClubId}");
                    break;
                case LoginDatabaseStatus.IncorrectPassword:
                    NAPI.Chat.SendChatMessageToPlayer(player, "That password is incorrect.");
                    break;
                case LoginDatabaseStatus.UnknownError:
                    NAPI.Chat.SendChatMessageToPlayer(player, "An unknown error occured.");
                    break;
            }
        }

        public CreateDatabaseStatus createAccount(Player player, String password)
        {
           using(var dbContext = new FiveDBContext())
            {
                if(dbContext.Accounts.Find(player.SocialClubId) == null)
                {
                    Account newPlayerAccount = new Account { SocialClubId = player.SocialClubId, Password = BCrypt.Net.BCrypt.HashPassword(password)};
                    dbContext.Accounts.Add(newPlayerAccount);
                    dbContext.SaveChanges();
                    return CreateDatabaseStatus.AccountCreated;
                } else
                {
                    return CreateDatabaseStatus.AccountAlreadyExists;
                }
            }
        }

       
        public Account LoginAccount(ulong socialClubID, String password, out LoginDatabaseStatus status )
        {
            using (var dbContext = new FiveDBContext())
            {
                Account targetAccount = dbContext.Accounts.Find(socialClubID);
                if(targetAccount == null)
                {
                    status = LoginDatabaseStatus.AccountDoesntExist;
                    return null;
                }

                if (BCrypt.Net.BCrypt.Verify(password, targetAccount.Password))
                {
                    status = LoginDatabaseStatus.Success;
                    return targetAccount;
                } else
                {
                    status = LoginDatabaseStatus.IncorrectPassword;
                    return null;
                }
            }
        }
    }
}
