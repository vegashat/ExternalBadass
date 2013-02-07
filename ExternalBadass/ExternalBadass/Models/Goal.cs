using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExternalBadass.Models
{
    public class Goal
    {
        public int GoalId { get; set; }
        public int UserId { get; set; }
        public int PointTotal { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime Deadline { get; set; }

        public User User { get; set; }
    }
}