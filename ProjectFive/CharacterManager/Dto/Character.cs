using ProjectFive.AccountManager;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectFive.CharacterManager
{
    public class Character
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CharacterId { get; set; }
        public String CharacterName { get; set; }
        public String Gender { get; set; }
        public int Age { get; set; }
        public String Race { get; set; }
        public String Job { get; set; }
        public int PlayingHours { get; set; }
        public long Money { get; set; }
        public ulong AccountSocialClubId { get; set; }

        public String GetDisplayName()
        {
            return this.CharacterName.Replace("_", " ");
        }
    }
}
