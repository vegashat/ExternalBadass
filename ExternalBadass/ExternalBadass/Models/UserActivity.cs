using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ExternalBadass.Models
{
    public class UserActivity
    {
        [Key, Column(Order = 0)]
        public int UserId { get; set; }
        [Key, Column(Order = 1)]
        public int ActivityId { get; set; }

        public User User { get; set; }
        public Activity Activity { get; set; }
    }
}