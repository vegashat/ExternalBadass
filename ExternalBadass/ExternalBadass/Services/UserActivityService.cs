using ExternalBadass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExternalBadass.Services
{
    public class UserActivityService
    {
        BadassContext _context { get; set; }
        public UserActivityService(BadassContext context)
        {
            _context = context;
        }

        public void CalculateIncentives(string username)
        {
            var incentives = _context.Incentives.Where(i => i.User.Username == username);

            foreach(var incentive in incentives.ToList())
            {
                var activities = _context.UserActivities.Where(ua => ua.User.Username == username && ua.Date >= incentive.StartDate && ua.Date <= incentive.Deadline);

                incentive.PointsEarned = activities.Sum(a => a.Activity.PointValue);
            }

            _context.SaveChanges();
        }
        
    }
}