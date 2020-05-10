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

        [ServerEvent(Event.ResourceStart)]
        public void HandleAccountHandlerStart()
        {
            NAPI.Util.ConsoleOutput("[Account Handler] Account Handler has successfully booted up!");
            using(var dbContext = new FiveDBContext())
            {
                NAPI.Util.ConsoleOutput($"There are {dbContext.Accounts.Count()} users created in the database!");
            }
        }


        [Command("signup")]
        public void CreateAccount(Player player, String password)
        {
            CreateDatabaseStatus returnedResult = createAccount(player, password);

            switch (returnedResult)
            {
                case CreateDatabaseStatus.AccountCreated:
                    NAPI.Chat.SendChatMessageToPlayer(player, "Succesfully created your account!");
                    return;
                case CreateDatabaseStatus.AccountAlreadyExists:
                    NAPI.Chat.SendChatMessageToPlayer(player, "Looks like your account already exists...");
                    return;
                default:
                    NAPI.Chat.SendChatMessageToPlayer(player, "An unknown error occured.");
                    return;
            }
        }

        [Command("login",SensitiveInfo = true)]
        public void LoginUser(Player player, String password)
        {
            LoginDatabaseStatus status;
            Account playerAccount = LoginAccount(player.SocialClubId, password, out status);


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
                default:
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
