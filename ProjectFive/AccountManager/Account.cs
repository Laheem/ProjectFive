using MessagePack;
using ProjectFive.CharacterManager;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace ProjectFive.AccountManager
{
     public class Account
    {
        [Key]
        public ulong SocialClubId { get; set; }
        public bool IsAdmin { get; set; } = false;
        public bool IsDev { get; set; } = false;
        public bool IsVip { get; set; } = false;
        public bool IsBanned { get; set; } = false;
        public String Password { get; set; }
        public List<Character> Characters { get; set; }
        public String ServerRank { get; set; }
        public int VipLevel { get; set; }
        public DateTime? VipExpiration { get; set; }
        public int VipTokens { get; set; }
        public String SocialClubName { get; set; }


        public String getRemainingDaysOfVip()
        {
            if(VipExpiration == null)
            {
                return "0 Days";
            }
            return (VipExpiration.Value.Date - DateTime.Now.Date).Days.ToString() + " Days";
        }
    }
}