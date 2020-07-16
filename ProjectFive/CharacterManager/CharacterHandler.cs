using GTANetworkAPI;
using ProjectFive.AccountManager;
using ProjectFive.CharacterManager.Dto;
using ProjectFive.CharacterManager.Service;
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
        private CharacterEntityService characterEntityService = new CharacterEntityService();

        // TODO - FIX THIS UGLY HACK - Static events bad but not too sure of another way...
        public static event EventHandler CharacterLoggedIn;

        protected virtual void OnCharacterLoggedIn(CharacterLogInArgs e)
        {
            EventHandler handler = CharacterLoggedIn;
            if(handler == null)
            {
                NAPI.Chat.SendChatMessageToAll("still null");
            }
            handler?.Invoke(this, e);
        }

        [Command("createcharacter", GreedyArg = true)]
        public void CreateCharacter(Player player, String name)
        {
            if (player.HasData(DataKeys.ACCOUNT_KEY))
            {
                Account playerAccount = player.GetData<Account>(DataKeys.ACCOUNT_KEY);
                try
                {
                    if (!ValidateCharacterName(name))
                    {
                        NAPI.Chat.SendChatMessageToPlayer(player, "Your name doesn't match the required format. Please enter something similar to John_Smith");
                        return;
                    }
                    if (playerAccount != null)
                    {
                        Character newPlayerChar = new Character { CharacterName = name, AccountSocialClubId = playerAccount.SocialClubId, Age = 23, Gender = "M" };
                        characterService.CreateCharacter(newPlayerChar);
                        NAPI.Chat.SendChatMessageToPlayer(player, $"Character created! You are now playing as {newPlayerChar.CharacterName}");
                        OnCharacterLoggedIn(new CharacterLogInArgs(player, newPlayerChar, playerAccount));
                        characterEntityService.SetCurrentCharacter(player, newPlayerChar);
                    }
                    else
                    {
                        NAPI.Chat.SendChatMessageToPlayer(player, "There was an error processing your character.");
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
                    Character chosenCharacter = allCharacters[selectedIndex - 1];
                    characterEntityService.SetCurrentCharacter(player, chosenCharacter);
                    NAPI.Chat.SendChatMessageToPlayer(player, $"You've selected {chosenCharacter.CharacterName}");
                    OnCharacterLoggedIn(new CharacterLogInArgs(player, chosenCharacter, playerAccount));
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

        [Command("stats")]
        public void GetCharacterStats(Player player)
        {
            if (characterEntityService.HasSelectedCharacter(player))
            {
                GenerateStatsPage(player, characterEntityService.GetCharacter(player));
            }
        }


        private bool ValidateCharacterName(String characterName)
        {
            Regex regex = new Regex("^[A-Z]{1}[a-z]{1,}_[A-Z]{1}[a-z]{1,}$");
            return regex.IsMatch(characterName);
        }


        private void GenerateStatsPage(Player player,Character character)
        {
            Account playerAccount = player.GetData<Account>(DataKeys.ACCOUNT_KEY);
            NAPI.Chat.SendChatMessageToPlayer(player, new string('-', 90));
            NAPI.Chat.SendChatMessageToPlayer(player, $"{character.CharacterName} | Age: {character.Age} | Race: Placeholder | Money: ${character.Money} | Bank Money: $3");
            NAPI.Chat.SendChatMessageToPlayer(player, "Job: Placeholder | Faction: Vimto Syndacrips | Rank: Original Fizzy (10)");
            NAPI.Chat.SendChatMessageToPlayer(player, "Houses: None | Businesses: 23, 222");
            NAPI.Chat.SendChatMessageToPlayer(player, $"Playing Hours : {character.PlayingHours} | Rank: very cool | VIP: {playerAccount.VipLevel} | VIP Expiration : {playerAccount.GetRemainingDaysOfVip()}");
            NAPI.Chat.SendChatMessageToPlayer(player, new string('-', 90));

        }
    }
}