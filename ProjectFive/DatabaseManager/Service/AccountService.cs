using GTANetworkAPI;
using ProjectFive.AccountManager;
using ProjectFive.DatabaseManager.Repository;
using System;
using System.Threading.Tasks;

namespace ProjectFive.DatabaseManager
{
    internal class AccountService
    {
        private readonly AccountRepository accountRepo = new AccountRepository();

        public Account GetPlayerAccount(Player player)
        {
            Task<Account> accountTask = accountRepo.GetAccountBySocialClubId(player.SocialClubId);

            try
            {
                accountTask.Wait(TimeSpan.FromSeconds(20));

                if (accountTask.IsCompleted)
                {
                    return accountTask.Result;
                } else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }


        }

        public CreateDatabaseStatus CreateAccount(Account account)
        {
            Task<int> accountCreateTask = accountRepo.CreateAccount(account);
            try
            {
                accountCreateTask.Wait(TimeSpan.FromSeconds(20));

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return CreateDatabaseStatus.ErrorOccured;
            }

            if (accountCreateTask.IsCompleted)
            {
                if (accountCreateTask.Result == -1)
                {
                    return CreateDatabaseStatus.AccountAlreadyExists;
                }

                return CreateDatabaseStatus.AccountCreated;
            } else
            {
                return CreateDatabaseStatus.ErrorOccured;
            }
        }

        public bool UpdateAccount(Account account)
        {
            Task<int> accountUpdateTask = accountRepo.UpdateAccount(account);
            try
            {
                accountUpdateTask.Wait(TimeSpan.FromSeconds(20));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            if (accountUpdateTask.IsCompleted)
            {
                return true;
            }

            return false;
        }

        public Account LoginAccount(ulong socialClubID, String password, out LoginDatabaseStatus status)
        {
            Task<Account> accountTask = accountRepo.GetAccountBySocialClubId(socialClubID);

            try
            {
                accountTask.Wait(TimeSpan.FromSeconds(20));

                if (accountTask.IsCompleted)
                {
                    if (BCrypt.Net.BCrypt.Verify(password, accountTask.Result.Password))
                    {
                        status = LoginDatabaseStatus.Success;
                        return accountTask.Result;
                    }

                    status = LoginDatabaseStatus.IncorrectPassword;
                    return null;
                }
                else
                {
                    status = LoginDatabaseStatus.UnknownError;
                    return null;
                }
            }
            catch (Exception)
            {
                status = LoginDatabaseStatus.UnknownError;
                return null;
            }
        }
    }
}