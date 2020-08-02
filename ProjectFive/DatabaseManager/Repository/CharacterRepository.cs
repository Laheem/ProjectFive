using Microsoft.EntityFrameworkCore;
using ProjectFive.AccountManager;
using ProjectFive.CharacterManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectFive.DatabaseManager.Repository
{
    internal class CharacterRepository
    {
        public async Task<int> CreateCharacter(Character playerCharacter)
        {
            try
            {
                using var dbContext = new FiveDBContext();
                var databaseCharacter = await dbContext.Characters.FindAsync(playerCharacter.CharacterId).ConfigureAwait(false);
                if (databaseCharacter == default)
                {
                    dbContext.Characters.Add(playerCharacter);
                    return await dbContext.SaveChangesAsync().ConfigureAwait(false);
                }

                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> UpdateCharacter(Character characterToUpdate)
        {
            try
            {
                using var dbContext = new FiveDBContext();
                var databaseCharacter = await dbContext.Characters.FindAsync(characterToUpdate.CharacterId).ConfigureAwait(false);
                if (databaseCharacter != default)
                {
                    dbContext.Entry(databaseCharacter).CurrentValues.SetValues(characterToUpdate);
                    return await dbContext.SaveChangesAsync().ConfigureAwait(false);
                }
                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> DeleteCharacter(Character characterToDelete)
        {
            try
            {
                using var dbContext = new FiveDBContext();
                var databaseAccount = await dbContext.Accounts.FindAsync(characterToDelete.CharacterId);
                if (databaseAccount != default)
                {
                    dbContext.Remove(characterToDelete);
                    return await dbContext.SaveChangesAsync().ConfigureAwait(false);
                }

                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Character>> GetAllCharactersForAccount(Account playerAccount)
        {
            try
            {
                using var dbContext = new FiveDBContext();
                return await dbContext.Characters.Where(character => character.AccountSocialClubId == playerAccount.SocialClubId).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Character> GetCharacter(int characterId)
        {
            try
            {
                using var dbContext = new FiveDBContext();
                return await dbContext.Characters.FindAsync(characterId);
            }
            catch
            {
                throw;
            }
        }

        public async Task<Character> GetCharacterByName(string CharacterName)
        {
           
           using var dbContext = new FiveDBContext();
           return await dbContext.Characters.FirstAsync(character => String.Equals(CharacterName,character.CharacterName,StringComparison.OrdinalIgnoreCase));
            
        }
    }
}