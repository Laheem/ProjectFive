using GTANetworkAPI;
using ProjectFive.AccountManager;
using ProjectFive.CharacterManager;
using ProjectFive.DatabaseManager.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectFive.DatabaseManager.Service
{
    internal class CharacterService
    {
        private CharacterRepository characterRepository = new CharacterRepository();

        public CreateDatabaseStatus CreateCharacter(Character playerCharacter)
        {
            Task<int> characterCreateTask = characterRepository.CreateCharacter(playerCharacter);
            try
            {
                characterCreateTask.Wait(TimeSpan.FromSeconds(20));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return CreateDatabaseStatus.ErrorOccured;
            }

            if (characterCreateTask.IsCompleted)
            {
                return CreateDatabaseStatus.AccountCreated;
            }
            else
            {
                return CreateDatabaseStatus.ErrorOccured;
            }
        }

        public Character GetCharacter(int characterId)
        {
            Task<Character> getCharacterTask = characterRepository.GetCharacter(characterId);

            try
            {
                getCharacterTask.Wait(TimeSpan.FromSeconds(20));

                if (getCharacterTask.IsCompleted)
                {
                    return getCharacterTask.Result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool SaveCharacter(Character character)
        {
            Task<int> characterCreateTask = characterRepository.UpdateCharacter(character);
            try
            {
                characterCreateTask.Wait(TimeSpan.FromSeconds(20));
            }
            catch (Exception e)
            {
                NAPI.Util.ConsoleOutput(e.StackTrace);
                return false;
            }

            return true;
        }

        public Character GetCharacter(String characterName)
        {
            Task<Character> getCharacterTask = characterRepository.GetCharacterByName(characterName);

            try
            {
                getCharacterTask.Wait(TimeSpan.FromSeconds(20));

                if (getCharacterTask.IsCompleted)
                {
                    return getCharacterTask.Result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }

        }

        public List<Character> GetAllCharacters(Account account)
        {
            Task<List<Character>> getAllCharactersTask = characterRepository.GetAllCharactersForAccount(account);

            try
            {
                getAllCharactersTask.Wait(TimeSpan.FromSeconds(20));

                if (getAllCharactersTask.IsCompleted)
                {
                    return getAllCharactersTask.Result;
                } else
                {
                    return new List<Character>();
                }
            }
            catch (Exception)
            {
                return new List<Character>();
            }
        }
    }
}