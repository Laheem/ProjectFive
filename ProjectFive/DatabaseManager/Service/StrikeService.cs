using ProjectFive.AccountManager;
using ProjectFive.DatabaseManager.Repository;
using ProjectFive.StrikeManager.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectFive.DatabaseManager.Service
{
    class StrikeService
    {
        StrikeRepository strikeRepository = new StrikeRepository();


        public bool CreateStrike(Strike strike)
        {
            Task<int> strikeCreateTask = strikeRepository.CreateStrike(strike);
            try
            {
                strikeCreateTask.Wait(TimeSpan.FromSeconds(20));

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            return strikeCreateTask.IsCompleted;
        }

        public bool DeleteStrike(int id)
        {

            Task<int> strikeDeleteTask = strikeRepository.DeleteStrike(id);
            try
            {
                strikeDeleteTask.Wait(TimeSpan.FromSeconds(20));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            return strikeDeleteTask.IsCompleted;
        }


        public List<Strike> GetStrikes(Account account)
        {
            Task<List<Strike>> getStrikesTask = strikeRepository.GetAllStrikesForAccount(account);

            try
            {
                getStrikesTask.Wait(TimeSpan.FromSeconds(20));

                if (getStrikesTask.IsCompleted)
                {
                    return getStrikesTask.Result;
                }
                else
                {
                    return new List<Strike>();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<Strike>();
            }
        }

    }
}
