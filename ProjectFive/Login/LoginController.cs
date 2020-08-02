using GTANetworkAPI;
using ProjectFive.AccountManager;
using ProjectFive.CharacterManager;
using ProjectFive.DatabaseManager;
using ProjectFive.DatabaseManager.Service;
using ProjectFive.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectFive.Login
{
    class LoginController : Script
    {

        AccountService accountService = new AccountService();
        AccountEntityService accountEntityService = new AccountEntityService();

        CharacterController characterController = new CharacterController();
        CharacterService characterService = new CharacterService();


        [ServerEvent(Event.ResourceStart)]
        public void DisableUserSpawn()
        {
            NAPI.Server.SetAutoSpawnOnConnect(false);
            NAPI.Util.ConsoleOutput("[LOGIN] - Login has booted successfuly.");
        }

        [ServerEvent(Event.PlayerConnected)]
        public void onPlayerConnected(Player player)
        {
            if(accountService.GetAccount(player) == null)
            {
                ChatUtils.SendInfoMessage(player, "Normally you'd create an account here, but we haven't done that yet. Spawning you in! Please use /signup for now.");
                NAPI.Player.SpawnPlayer(player, new Vector3(0, 0, 60));
            } else
            {
                NAPI.ClientEvent.TriggerClientEvent(player, "login", player.SocialClubId, player.SocialClubName);
                player.Dimension = 555;
                player.Transparency = 0;
            }
        }


        public void onSignInSuccessful(Player player, List<String> characterNames)
        {
            String jsonList = NAPI.Util.ToJson(characterNames);
            NAPI.ClientEvent.TriggerClientEvent(player, "loginSuccess", jsonList);
        }

        public void onFailedSignIn(Player player)
        {
            // TODO - Call client event here.
        }

        [RemoteEvent("attemptLogin")]
        public void onPasswordSubmitted(Player player, object[] args)
        {
            if (SignInAccount(player,args[0].ToString()))
            {
                Account playerAccount = accountEntityService.GetAccount(player);
                onSignInSuccessful(player, characterService.GetAllCharacters(playerAccount).Select(c =>c.CharacterName).ToList());
            } else
            {
                onFailedSignIn(player);
            }
        }


        [RemoteEvent("selectCharacter")]
        public void onCharacterSelected(Player player, object[] args)
        {
            Account playerAccount = accountEntityService.GetAccount(player);
            Character character = characterService.GetAllCharacters(playerAccount).First(c => c.CharacterName == args[0].ToString());
            characterController.CharacterSelected(player, playerAccount, character);
            player.Transparency = 255;
            player.Dimension = 0;

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