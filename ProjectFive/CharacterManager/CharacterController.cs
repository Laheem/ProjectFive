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
    internal class CharacterController : Script
    {
        private CharacterService characterService = new CharacterService();
        private CharacterEntityService characterEntityService = new CharacterEntityService();
        private AccountEntityService accountEntityService = new AccountEntityService();

        // TODO - FIX THIS UGLY HACK - Static events bad but not too sure of another way...
        public static event EventHandler CharacterLoggedIn;

        protected virtual void OnCharacterLoggedIn(CharacterLogInArgs e)
        {
            EventHandler handler = CharacterLoggedIn;
            handler?.Invoke(this, e);
        }

        // TODO - Validate character stuff from clientside.
        [RemoteEvent("characterSelectValidate")]
        public void createCharacter(Player player, object[] args)
        {
            Account playerAccount = accountEntityService.GetAccount(player);
            RequestCharacterCreation(player, args[0].ToString(), int.Parse(args[2].ToString()), args[1].ToString(), playerAccount);
        }

        private void RequestCharacterCreation(Player player, string name, int age, String gender, Account playerAccount)
        {
            // TODO - Create generic spawn point once we get around to that.
            Character newPlayerChar = new Character
            {
                CharacterName = name,
                AccountSocialClubId = playerAccount.SocialClubId,
                Age = age,
                Gender = gender,
                Health = 100,
                PositionX = -407.49152f,
                PositionY = 1185.177f,
                PositionZ = 325.53732f
            };
            characterService.CreateCharacter(newPlayerChar);
            ChatUtils.SendInfoMessage(player, $"Character created! You are now playing as {newPlayerChar.CharacterName}");
            SpawnPlayer(player, playerAccount, newPlayerChar);

        }


        public void CharacterSelected(Player player, Account playerAccount, Character chosenCharacter)
        {
            ChatUtils.SendInfoMessage(player, $"You've selected {chosenCharacter.CharacterName}");
            SpawnPlayer(player, playerAccount, chosenCharacter);
        }

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


        private void GenerateStatsPage(Player player, Character character)
        {
            Account playerAccount = player.GetData<Account>(DataKeys.ACCOUNT_KEY);
            ChatUtils.SendInfoMessage(player, new string('-', 90));
            ChatUtils.SendInfoMessage(player, $"{character.CharacterName} | Age: {character.Age} | Gender: {character.Gender} | Money: ${character.Money} | Bank Money: $3");
            ChatUtils.SendInfoMessage(player, "Job: Placeholder | Faction: Vimto Syndacrips | Rank: Original Fizzy (10)");
            ChatUtils.SendInfoMessage(player, "Houses: None | Businesses: 23, 222");
            ChatUtils.SendInfoMessage(player, $"Playing Hours : {character.PlayingHours} | Rank: very cool | VIP: {playerAccount.VipLevel} | VIP Expiration : {playerAccount.GetRemainingDaysOfVip()}");
            ChatUtils.SendInfoMessage(player, new string('-', 90));

        }

        private void SpawnPlayer(Player player, Account playerAccount, Character newPlayerChar)
        {
            OnCharacterLoggedIn(new CharacterLogInArgs(player, newPlayerChar, playerAccount));
            characterEntityService.SetCurrentCharacter(player, newPlayerChar);
            player.Transparency = 255;
            player.Dimension = 1;
        }
    }
}   