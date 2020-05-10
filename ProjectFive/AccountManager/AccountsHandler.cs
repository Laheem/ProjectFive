using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTANetworkAPI;
using ProjectFive.DatabaseManager;

namespace ProjectFive.AccountManager
{
    class AccountsHandler : Script
    {

        [ServerEvent(Event.ResourceStart)]
        public void HandleAccountHandlerStart()
        {
            NAPI.Util.ConsoleOutput("[Account Handler] Account Handler has successfully booted up!");

        
        }


        [Command("signup")]
        public void CreateAccount(Player player, String password)
        {
            DatabaseStatus returnedResult = createAccount(player, password);

            switch (returnedResult)
            {
                case DatabaseStatus.ACCOUNTCREATED:
                    NAPI.Chat.SendChatMessageToPlayer(player, "Succesfully created your account!");
                    return;
                case DatabaseStatus.ACCOUNTALREADYEXISTS:
                    NAPI.Chat.SendChatMessageToPlayer(player, "Looks like your account already exists...");
                    return;
                default:
                    NAPI.Chat.SendChatMessageToPlayer(player, "An unknown error occured.");
                    return;
            }
        }


        public DatabaseStatus createAccount(Player player, String password)
        {
           
           using(var dbContext = new FiveDBContext())
            {
                if(dbContext.Accounts.Find(player.SocialClubId) == null)
                {
                    Account newPlayerAccount = new Account { SocialClubId = player.SocialClubId, Password = password };
                    dbContext.Accounts.Add(newPlayerAccount);
                    dbContext.SaveChanges();
                    return DatabaseStatus.ACCOUNTCREATED;
                } else
                {
                    return DatabaseStatus.ACCOUNTALREADYEXISTS;
                }
            }
        }






    }
}
