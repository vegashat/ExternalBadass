using ExternalBadass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExternalBadass.Services
{
    public class ActivityService
    {
        BadassContext _context;

        public ActivityService()
        {
            _context = new BadassContext();
        }

        public IEnumerable<Activity> GetActivities()
        {
            return _context.Activities;

        }
       
        public void SaveActivity(Activity activity)
        {

            if (activity.ActivityId == 0)
            {
                _context.Activities.Add(activity);
            }
            else
            {
                _context.Activities.Attach(activity);
                _context.Entry<Activity>(activity).State = System.Data.EntityState.Modified;
            }

            _context.SaveChanges();
        }

        
    }
}