using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExternalBadass.Models;
using ExternalBadass.ViewModels;

namespace ExternalBadass.Controllers
{
    public class UserActivityController : Controller
    {
        private BadassContext db = new BadassContext();

        //
        // GET: /UserActivity/vegashat
        public ActionResult Index(string username)
        {
            IEnumerable<UserActivity> activities = null;

            if (db.Users.Any(u => u.Username == username))
            {
                activities = db.Users.FirstOrDefault(u => u.Username == username).Activities.ToList();
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
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", new { username = userActivity.User.Username });
        }

        //
        // GET: /UserActivity/Edit/5

        public ActionResult Edit(int id = 0)
        {
            UserActivity useractivity = db.UserActivities.Find(id);
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
            return View(useractivity);
        }

        //
        // GET: /UserActivity/Delete/5

        public ActionResult Delete(int id = 0)
        {
            UserActivity useractivity = db.UserActivities.Find(id);
            if (useractivity == null)
            {
                return HttpNotFound();
            }
            return View(useractivity);
        }

        //
        // POST: /UserActivity/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            UserActivity useractivity = db.UserActivities.Find(id);
            db.UserActivities.Remove(useractivity);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}