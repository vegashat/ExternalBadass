using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExternalBadass.Models;
using ExternalBadass.ViewModels;
using ExternalBadass.Services;

namespace ExternalBadass.Controllers
{
    public class UserActivityController : Controller
    {
        private BadassContext db = new BadassContext();
        UserActivityService _uaService = null;

        public UserActivityController()
        {
             _uaService = new UserActivityService(db);
        }

        //
        // GET: /UserActivity/vegashat
        public ActionResult Index(string username)
        {
            IEnumerable<UserActivity> activities = null;

            if (db.Users.Any(u => u.Username == username))
            {
                activities = db.UserActivities.Include(ua => ua.User).Include(ua => ua.Activity).Where(ua => ua.User.Username == username);
            }

            ViewBag.Username = username;

            return View(activities);
        }

        //
        // GET: /UserActivity/Details/5

        public ActionResult Details(int id = 0)
        {
            UserActivity useractivity = db.UserActivities.Find(id);
            if (useractivity == null)
            {
                return HttpNotFound();
            }
            return View(useractivity);
        }

        //
        // GET: /UserActivity/Create

        public ActionResult Create(string username)
        {

            var users = db.Users;
            var activities = db.Activities;
            var currentUser = users.Include(u => u.Activities).First(u => u.Username == username);

            var model = new UserActivityViewModel(users, activities, currentUser);

            return View(model);
        }

        //
        // POST: /UserActivity/Create

        [HttpPost]
        public ActionResult Create(UserActivity userActivity)
        {
            if (ModelState.IsValid)
            {
                userActivity.UserId = userActivity.User.UserId;
                userActivity.ActivityId = userActivity.Activity.ActivityId;

                userActivity.User = db.Users.Find(userActivity.UserId);
                userActivity.Activity = db.Activities.Find(userActivity.ActivityId);

                db.UserActivities.Add(userActivity);
                db.SaveChanges();

                _uaService.CalculateIncentives(userActivity.User.Username);

                return RedirectToAction("Index", new { username = userActivity.User.Username });
                
            }

            return RedirectToAction("Index", new { username = userActivity.User.Username });
        }

        //
        // GET: /UserActivity/Edit/5

        public ActionResult Edit(int userId, int activityId, DateTime date)
        {
            UserActivity useractivity = db.UserActivities.FirstOrDefault(ua => ua.UserId == userId && ua.ActivityId == activityId && ua.Date == date);
            if (useractivity == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", useractivity.UserId);
            ViewBag.ActivityId = new SelectList(db.Activities, "ActivityId", "Name", useractivity.ActivityId);
            return View(useractivity);
        }

        //
        // POST: /UserActivity/Edit/5

        [HttpPost]
        public ActionResult Edit(UserActivity useractivity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(useractivity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", useractivity.UserId);
            ViewBag.ActivityId = new SelectList(db.Activities, "ActivityId", "Name", useractivity.ActivityId);


            _uaService.CalculateIncentives(useractivity.User.Username);

            return View(useractivity);
        }

        public ActionResult Delete(int userId, int activityId, DateTime date)
        {
            UserActivity useractivity = db.UserActivities.Include(ua => ua.User).FirstOrDefault(ua => ua.UserId == userId && ua.ActivityId == activityId && ua.Date == date);
            var username = useractivity.User.Username;
            
            db.UserActivities.Remove(useractivity); 
            db.SaveChanges();

            _uaService.CalculateIncentives(username);

            return RedirectToAction("Index", new { username = username });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}