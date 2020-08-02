using Microsoft.EntityFrameworkCore;
using ProjectFive.AccountManager;
using ProjectFive.StrikeManager.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectFive.DatabaseManager.Repository
{
    class StrikeRepository
    {

        public async Task<int> CreateStrike(Strike strike)
        {
            try
            {
                using var dbContext = new FiveDBContext();
                dbContext.Strikes.Add(strike);
                return await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
        }

        public async Task<int> DeleteStrike(int id)
        {
            try
            {
                using var dbContext = new FiveDBContext();
                var databaseStrike = await dbContext.Strikes.FindAsync(id);
                if (databaseStrike != default)
                {
                    dbContext.Remove(databaseStrike);
                    return await dbContext.SaveChangesAsync().ConfigureAwait(false);
                }

                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
        }

        public async Task<List<Strike>> GetAllStrikesForAccount(Account playerAccount)
        {
            try
            {
                using var dbContext = new FiveDBContext();
                return await dbContext.Strikes.Where(strike => strike.AccountSocialClubId == playerAccount.SocialClubId).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<Strike>();
            }
        }



    }
}
