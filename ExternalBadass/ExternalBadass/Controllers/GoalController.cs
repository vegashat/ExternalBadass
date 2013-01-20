using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExternalBadass.Models;

namespace ExternalBadass.Controllers
{
    public class GoalController : Controller
    {
        private BadassContext db = new BadassContext();

        //
        // GET: /Goal/

        public ActionResult Index()
        {
            var goals = db.Goals.Include(g => g.User).Include(g => g.Activity);
            return View(goals.ToList());
        }

        //
        // GET: /Goal/Details/5

        public ActionResult Details(int id = 0)
        {
            Goal goal = db.Goals.Find(id);
            if (goal == null)
            {
                return HttpNotFound();
            }
            return View(goal);
        }

        //
        // GET: /Goal/Create

        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username");
            ViewBag.ActivityId = new SelectList(db.Activities, "ActivityId", "Name");
            return View();
        }

        //
        // POST: /Goal/Create

        [HttpPost]
        public ActionResult Create(Goal goal)
        {
            if (ModelState.IsValid)
            {
                db.Goals.Add(goal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", goal.UserId);
            ViewBag.ActivityId = new SelectList(db.Activities, "ActivityId", "Name", goal.ActivityId);
            return View(goal);
        }

        //
        // GET: /Goal/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Goal goal = db.Goals.Find(id);
            if (goal == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", goal.UserId);
            ViewBag.ActivityId = new SelectList(db.Activities, "ActivityId", "Name", goal.ActivityId);
            return View(goal);
        }

        //
        // POST: /Goal/Edit/5

        [HttpPost]
        public ActionResult Edit(Goal goal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(goal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", goal.UserId);
            ViewBag.ActivityId = new SelectList(db.Activities, "ActivityId", "Name", goal.ActivityId);
            return View(goal);
        }

        //
        // GET: /Goal/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Goal goal = db.Goals.Find(id);
            if (goal == null)
            {
                return HttpNotFound();
            }
            return View(goal);
        }

        //
        // POST: /Goal/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Goal goal = db.Goals.Find(id);
            db.Goals.Remove(goal);
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