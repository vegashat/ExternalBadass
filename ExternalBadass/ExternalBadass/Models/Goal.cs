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
        public int ActivityId { get; set; }
        public int Quantity { get; set; }
        public DateTime Deadline { get; set; }

        public User User { get; set; }
        public Activity Activity { get; set; }
    }
}