using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFive.CharacterManager
{
    public class Character
    {
        public ulong SocialClubId { get; set; }
        public String CharacterName { get; set; }
        public String Gender { get; set; }
        public int Age { get; set; }
        public String Race { get; set; }
        public String Job { get; set; }
        public int PlayingHours { get; set; }
        public long Money { get; set; }
        public String ServerRank { get; set; }
        public int VipLevel { get; set; }
        public DateTime VipExpiration { get; set; }
        public int VipTokens { get; set; }

    }
}
