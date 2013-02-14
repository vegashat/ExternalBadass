using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExternalBadass.Models
{
    public class Incentive
    {
        public int IncentiveId { get; set; }
        public int UserId { get; set; }
        public int PointTotal { get; set; }
        public int PointsEarned { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:P2}")]
        public decimal PercentComplete
        {
            get
            {
                var percent =  ((decimal)PointsEarned / (decimal)PointTotal);

                return Math.Round(percent, 2);
            }
        }

        public DateTime StartDate { get; set; }
        public DateTime Deadline { get; set; }
        public string Description { get; set; }

        public User User { get; set; }
        
    }
}