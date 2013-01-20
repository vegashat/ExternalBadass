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
    public class IncentiveController : Controller
    {
        private BadassContext db = new BadassContext();

        //
        // GET: /Incentive/

        public ActionResult Index()
        {
            var incentives = db.Incentives.Include(i => i.User);
            return View(incentives.ToList());
        }

        //
        // GET: /Incentive/Details/5

        public ActionResult Details(int id = 0)
        {
            Incentive incentive = db.Incentives.Find(id);
            if (incentive == null)
            {
                return HttpNotFound();
            }
            return View(incentive);
        }

        //
        // GET: /Incentive/Create

        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username");
            return View();
        }

        //
        // POST: /Incentive/Create

        [HttpPost]
        public ActionResult Create(Incentive incentive)
        {
            if (ModelState.IsValid)
            {
                db.Incentives.Add(incentive);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", incentive.UserId);
            return View(incentive);
        }

        //
        // GET: /Incentive/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Incentive incentive = db.Incentives.Find(id);
            if (incentive == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", incentive.UserId);
            return View(incentive);
        }

        //
        // POST: /Incentive/Edit/5

        [HttpPost]
        public ActionResult Edit(Incentive incentive)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incentive).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", incentive.UserId);
            return View(incentive);
        }

        //
        // GET: /Incentive/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Incentive incentive = db.Incentives.Find(id);
            if (incentive == null)
            {
                return HttpNotFound();
            }
            return View(incentive);
        }

        //
        // POST: /Incentive/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Incentive incentive = db.Incentives.Find(id);
            db.Incentives.Remove(incentive);
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