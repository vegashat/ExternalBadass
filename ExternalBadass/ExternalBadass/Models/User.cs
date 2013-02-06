using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExternalBadass.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public Gender Gender { get; set; }
        public int Age {
            get
            {
                DateTime now = DateTime.Today;
                int age = now.Year - Birthday.Year;
                if (now < Birthday.AddYears(age)) age--;

                return age;
            }
        }
        public string Email { get; set; }

        public virtual ICollection<Measurement> Measurements { get; set; }
        //public virtual ICollection<Goal> Goals { get; set; }
        public virtual ICollection<UserActivity> Activities { get; set; }
        public virtual ICollection<Incentive> Incentives { get; set; }

    }
}