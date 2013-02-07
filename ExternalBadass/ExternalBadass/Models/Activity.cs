using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExternalBadass.Models
{
    public class Activity
    {
        public int ActivityId { get; set; }
        public string Name { get; set; }
        public int PointValue { get; set; }

        public virtual ICollection<UserActivity> UserActivites { get; set; }
    }
}