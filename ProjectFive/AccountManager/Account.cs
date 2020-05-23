﻿using MessagePack;
using System;
using System.ComponentModel.DataAnnotations;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace ProjectFive.AccountManager
{
     class Account
    {
        [Key]
        public ulong SocialClubId { get; set; }
        public bool IsAdmin { get; set; } = false;
        public bool IsDev { get; set; } = false;
        public bool IsVip { get; set; } = false;
        public bool IsBanned { get; set; } = false;
        public String Password { get; set; }
    }
}