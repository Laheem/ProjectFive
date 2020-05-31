using ProjectFive.AccountManager;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFive.AdminManager
{
    class AdminController
    {
        const int MAX_ADMIN_LEVEL = 5;
        const int MIN_ADMIN_LEVEL = 1;
        DatabaseManager.AccountService accountService = new DatabaseManager.AccountService();
        public void SetAdminLevel(Account account, int adminLevel)
        {
            account.AdminLevel = adminLevel;
            accountService.UpdateAccount(account);
        }

        public bool isValidAdminLevel(int adminLevel)
        {
            return adminLevel >= MIN_ADMIN_LEVEL && adminLevel <= MAX_ADMIN_LEVEL;
        }
    }
}
