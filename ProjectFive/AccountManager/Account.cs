using System;

namespace ProjectFive.AccountManager
{
    internal class Account
    {
        public String SocialClubName { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDev { get; set; }
        public bool IsVip { get; set; }
        public bool IsBanned { get; set; }
        public String Password { get; set; }

    }
}