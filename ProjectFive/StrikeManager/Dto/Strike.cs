using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectFive.StrikeManager.Dto
{
    public class Strike
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StrikeId { get; set; }
        public string Reason { get; set; }
        public string StrikingAdminName { get; set; }
        public bool IsDeactivated { get; set; } = false;
        public string DeactivateReason { get; set; } = "";
        public string? VoidingAdminName { get; set; } = "";
        public ulong AccountSocialClubId { get; set; }
        public bool OffenderAcknowledged { get; set; }


    }
}
