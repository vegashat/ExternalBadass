using ExternalBadass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExternalBadass.ViewModels
{

    public class IncentiveBreakdown
    {
        public Incentive Incentive { get; set; }

        public IList<ActivityTotal> ActivityTotals { get; set; }

    }

    public class ActivityTotal
    {

        public string Name { get; set; }
        public int PointTotal { get; set; }
    }

    public class CurrentStatusViewModel
    {
        public IEnumerable<Incentive> Incentives { get; set; }
        public IEnumerable<UserActivity> UserActivities { get; set; }
        public IEnumerable<User> Users { get; set; }

        public CurrentStatusViewModel(IEnumerable<Incentive> incentives, IEnumerable<UserActivity> userActivities, IEnumerable<User> users)
        {
            Incentives     = incentives;
            UserActivities = userActivities;
            Users          = users;
        }


        public IList<ActivityTotal> GetActivityTotals(DateTime beginDate, DateTime endDate)
        {
            activityTotals = new List<ActivityTotal>();

            var totals = UserActivities.Where(ua => ua.Date >= beginDate && ua.Date <= endDate).GroupBy(ua => ua.Activity.Name).Select(ua => new { Name = ua.Key, Total = ua.Sum(u => u.Activity.PointValue) });

            foreach (var total in totals)
            {
                activityTotals.Add(new ActivityTotal() { Name = total.Name, PointTotal = total.Total });
            }

            return activityTotals;
        }

        List<IncentiveBreakdown> _incentiveBreakdowns = null;
        public IEnumerable<IncentiveBreakdown> IncentiveBreakdowns
        {
            get
            {
                if (_incentiveBreakdowns == null)
                {
                    _incentiveBreakdowns = new List<IncentiveBreakdown>();
                    foreach (var incentive in Incentives)
                    {
                        var breakdown = new IncentiveBreakdown();

                        breakdown.Incentive = incentive;

                        //Just do complete/incomplete for nowre
                        //breakdown.ActivityTotals = GetActivityTotals(incentive.StartDate, incentive.Deadline);

                        breakdown.ActivityTotals = new List<ActivityTotal>();
                        breakdown.ActivityTotals.Add(new ActivityTotal() { Name = "Complete", PointTotal = GetActivityTotals(incentive.StartDate, incentive.Deadline).Sum(at => at.PointTotal) });

                        if (incentive.PointsEarned < incentive.PointTotal)
                        {
                            breakdown.ActivityTotals.Add(new ActivityTotal() { Name = "Incomplete", PointTotal = incentive.PointTotal - incentive.PointsEarned });
                        }

                        _incentiveBreakdowns.Add(breakdown);
                    }
                }

                return _incentiveBreakdowns;
            }
        }

        public List<ActivityTotal> activityTotals { get; set; }
    }
}