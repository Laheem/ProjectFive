using GTANetworkAPI;
using ProjectFive.AccountManager;
using ProjectFive.DatabaseManager.Service;
using ProjectFive.Utils;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ProjectFive.CharacterManager
{
    internal class CharacterHandler : Script
    {
        private CharacterService characterService = new CharacterService();


        [Command("createcharacter", GreedyArg = true)]
        public void CreateCharacter(Player player, String name)
        {
            if (player.HasData(DataKeys.ACCOUNT_KEY))
            {
                Account playerAccount = player.GetData<Account>(DataKeys.ACCOUNT_KEY);
                try
                {
                    if (playerAccount != null && ValidateCharacterName(name))
                    {
                        Character newPlayerChar = new Character { CharacterName = name, AccountSocialClubId = playerAccount.SocialClubId, Age = 23, Gender = "M" };
                        characterService.CreateCharacter(newPlayerChar);
                        NAPI.Chat.SendChatMessageToPlayer(player, $"Character created! You are now playing as {newPlayerChar.CharacterName}");
                    }
                    else
                    {
                        NAPI.Chat.SendChatMessageToPlayer(player, "There was an error processing your character. Check your name and attempt to log in again.");
                    }
                }
                catch (Exception)
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, "Something went wrong while creating your character. Please, try again.");
                }
            } else
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "Unable to get your account. Please log in.");
            }
        }

        [Command("pick", GreedyArg = true)]
        public void SelectCharacter(Player player, String index)
        {
            if (int.TryParse(index, out int selectedIndex) && player.HasData(DataKeys.ACCOUNT_KEY))
            {
                Account playerAccount = player.GetData<Account>(DataKeys.ACCOUNT_KEY);
                try
                {
                    List<Character> allCharacters = characterService.GetAllCharacters(playerAccount);
                    Character chosenChar = allCharacters[selectedIndex - 1];
                    player.SetData<Character>(DataKeys.CHARACTER_KEY, chosenChar);
                    NAPI.Chat.SendChatMessageToPlayer(player, $"You've selected {chosenChar.CharacterName}");
                }
                catch
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, "Something went wrong while getting your characters.");
                }
            } else
            {
                NAPI.Chat.SendChatMessageToPlayer(player,"Log in!");
            }
        }


        private bool ValidateCharacterName(String characterName)
        {
            Regex regex = new Regex("[A-Z]{1}[a-z]{1,}_[A-Z]{1}[a-z]{1,}");
            return regex.IsMatch(characterName);
        }
    }
}