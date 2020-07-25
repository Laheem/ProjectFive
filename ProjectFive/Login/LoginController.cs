using GTANetworkAPI;
using ProjectFive.AccountManager;
using ProjectFive.DatabaseManager;
using ProjectFive.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFive.Login
{
    class LoginController : Script
    {

        AccountService accountService = new AccountService();

        [ServerEvent(Event.ResourceStart)]
        public void DisableUserSpawn()
        {
            NAPI.Server.SetAutoSpawnOnConnect(false);
            NAPI.Util.ConsoleOutput("[LOGIN] - Login has booted successfuly.");
        }

        [ServerEvent(Event.PlayerConnected)]
        public void onPlayerConnected(Player player)
        {
            NAPI.ClientEvent.TriggerClientEvent(player, "login", player.SocialClubId, player.SocialClubName);
      
            player.Transparency = 0;
        }

        [Command("letsgo")]
        public void letsgo(Player player)
        {
            NAPI.ClientEvent.TriggerClientEvent(player, "login");
            player.Transparency = 0;
        }

        public void onSignInSuccessful(Player player)
        {
            NAPI.ClientEvent.TriggerClientEvent(player, "loginSuccess");
        }

        public void onFailedSignIn(Player player)
        {
            NAPI.ClientEvent.TriggerClientEvent(player, "loginFailed", player.SocialClubId, player.SocialClubName);

        }

        [RemoteEvent("attemptLogin")]
        public void onPasswordSubmitted(Player player, object[] args)
        {
            if (SignInAccount(player,args[0].ToString()))
            {
                onSignInSuccessful(player);
            } else
            {
                onFailedSignIn(player);
            }
        }


        public bool SignInAccount(Player player, string password)
        {
            Account playerAccount = accountService.LoginAccount(player.SocialClubId, password, out LoginDatabaseStatus status);

            switch (status)
            {
                case LoginDatabaseStatus.AccountDoesntExist:
                    ChatUtils.SendInfoMessage(player, "That account doesn't exist.");
                    return false;
                case LoginDatabaseStatus.Success:
                    ChatUtils.SendInfoMessage(player, $"Account logged in, welcome back {playerAccount.SocialClubName}!");
                    player.SetData<Account>(DataKeys.ACCOUNT_KEY, playerAccount);
                    return true;
                case LoginDatabaseStatus.IncorrectPassword:
                    ChatUtils.SendInfoMessage(player, "That password is incorrect.");
                    return false;
                case LoginDatabaseStatus.UnknownError:
                    ChatUtils.SendInfoMessage(player, "An unknown error occured.");
                    return false;
            }
            return false;
        }
    }
}