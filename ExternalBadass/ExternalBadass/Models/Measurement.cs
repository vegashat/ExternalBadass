using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExternalBadass.Models
{
    public class Measurement
    {
        public int MeasurementId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public decimal Weight { get; set; }
        public decimal BMI { get; set; }

        public User User { get; set; }
    }
}