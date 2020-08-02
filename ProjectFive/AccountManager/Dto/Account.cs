using ProjectFive.CharacterManager;
using ProjectFive.Migrations;
using ProjectFive.StrikeManager.Dto;
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
        public bool IsHeadDev { get; set; }
        public bool IsHeadNarrative { get; set; }
        public bool IsVip { get; set; } = false;
        public bool IsBanned { get; set; } = false;
        public String Password { get; set; }
        public List<Character> Characters { get; set; }
        public String ServerRank { get; set; }
        public int VipLevel { get; set; }
        public DateTime? VipExpiration { get; set; }
        public int VipTokens { get; set; }
        public String SocialClubName { get; set; }
        public int AdminLevel { get; set; } = -1;
        public List<Strike> Strikes { get; set; }
        public int StrikeLevel { get; set; }
        public String AdminName { get; set; } = "Test Admin";


        public String GetRemainingDaysOfVip()
        {
            if(VipExpiration == null)
            {
                return "0 Days";
            }
            return (VipExpiration.Value.Date - DateTime.Now.Date).Days.ToString() + " Days";
        }
    }
}