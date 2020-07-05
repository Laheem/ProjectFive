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
        public async Task<int> CreateAccount(Account account)
        {
            try
            {
                using var dbContext = new FiveDBContext();
                var databaseAccount = await dbContext.Accounts.FindAsync(account.SocialClubId).ConfigureAwait(false);
                if (databaseAccount == default)
                {
                    dbContext.Accounts.Add(account);
                    return await dbContext.SaveChangesAsync().ConfigureAwait(false);
                    
                }

                return -1;
            }
            catch (Exception e)
            {
                throw new Exception("An error has occured within CreateAccount. " + e.Message);
            }

        }
        public async Task<int> UpdateAccount(Account accountToUpdate)
        {
            try
            {
                using var dbContext = new FiveDBContext();
                var databaseAccount = await dbContext.Accounts.FindAsync(accountToUpdate.SocialClubId).ConfigureAwait(false);
                if (databaseAccount != default)
                {
                    dbContext.Entry(databaseAccount).CurrentValues.SetValues(accountToUpdate);
                    return await dbContext.SaveChangesAsync().ConfigureAwait(false);
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception("An error has occured within UpdateAccount. " + e.Message);
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