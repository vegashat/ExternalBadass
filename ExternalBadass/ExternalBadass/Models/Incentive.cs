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

        [Display(Name="Points")]
        public int PointTotal { get; set; }
        [Display(Name = "Points Earned")]
        public int PointsEarned { get; set; }
        
        [Display(Name="Percent")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:P2}")]
        public decimal PercentComplete
        {
            get
            {
                var percent =  ((decimal)PointsEarned / (decimal)PointTotal);

                return Math.Round(percent, 2);
            }
        }

        [Display(Name = "Starts")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Ends")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Deadline { get; set; }
        public string Description { get; set; }

        public User User { get; set; }
        
    }
}