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
        public bool HasExpired { get; set; }
        public bool isVoid { get; set; }
        public string? VoidReason { get; set; }
        public string? VoidingAdminName { get; set; }
        public DateTime ExpireTime { get; set; }
    }
}
