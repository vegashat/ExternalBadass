using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExternalBadass.Models
{
    public class Incentive
    {
        public int IncentiveId { get; set; }
        public int UserId { get; set; }
        public int PointTotal { get; set; }
        public DateTime Deadline { get; set; }
        public string Description { get; set; }

        public User User { get; set; }
        
    }
}