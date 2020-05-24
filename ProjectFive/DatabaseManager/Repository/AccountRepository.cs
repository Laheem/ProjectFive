using GTANetworkMethods;
using ProjectFive.AccountManager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectFive.DatabaseManager.Repository
{
    class AccountRepository
    {
        public async Task<int> CreateAccount(ulong socialClubId, String password)
        {
            try
            {
                using var dbContext = new FiveDBContext();
                var databaseAccount = await dbContext.Accounts.FindAsync(socialClubId).ConfigureAwait(false);
                if (databaseAccount == default)
                {
                    Account newPlayerAccount = new Account { SocialClubId = socialClubId, Password = BCrypt.Net.BCrypt.HashPassword(password) };
                    dbContext.Accounts.Add(newPlayerAccount);
                    return await dbContext.SaveChangesAsync().ConfigureAwait(false);
                    
                }

                return 0;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<int> UpdateAccount(Account accountToUpdate)
        {
            try
            {
                using var dbContext = new FiveDBContext();
                var databaseAccount = await dbContext.Accounts.FindAsync(accountToUpdate).ConfigureAwait(false);
                if (databaseAccount != default)
                {
                    dbContext.Update(accountToUpdate);
                    return await dbContext.SaveChangesAsync().ConfigureAwait(false);
                }
                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> DeleteAccount(Account accountToDelete)
        {
            try
            {
                using var dbContext = new FiveDBContext();
                var databaseAccount = await dbContext.Accounts.FindAsync(accountToDelete.SocialClubId);
                if (databaseAccount != default)
                {
                    dbContext.Remove(accountToDelete);
                    return await dbContext.SaveChangesAsync().ConfigureAwait(false);
                }

                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Account> GetAccountBySocialClubId(ulong socialClubID)
        {
            using var dbContext = new FiveDBContext();
            return await dbContext.Accounts.FindAsync(socialClubID).ConfigureAwait(false);
        }
    }
}