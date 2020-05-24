using ProjectFive.AccountManager;
using ProjectFive.DatabaseManager.Repository;
using System;
using System.Threading.Tasks;

namespace ProjectFive.DatabaseManager
{
    internal class AccountService
    {
        private readonly AccountRepository accountRepo = new AccountRepository();

        public CreateDatabaseStatus CreateAccount(ulong socialClubId, String password)
        {
            Task<int> accountCreateTask = accountRepo.CreateAccount(socialClubId, password);
            accountCreateTask.Wait(TimeSpan.FromSeconds(20));

            if (accountCreateTask.IsCompleted)
            {
                return CreateDatabaseStatus.AccountCreated;
            }
            else
            {
                return CreateDatabaseStatus.ErrorOccured;
            }
        }

        public Account LoginAccount(ulong socialClubID, String password, out LoginDatabaseStatus status)
        {
            Task<Account> accountTask = accountRepo.GetAccountBySocialClubId(socialClubID);

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
    }
}