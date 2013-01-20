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
    public class MeasurementController : Controller
    {
        private BadassContext db = new BadassContext();

        //
        // GET: /Measurement/

        public ActionResult Index()
        {
            var measurements = db.Measurements.Include(m => m.User);
            return View(measurements.ToList());
        }

        //
        // GET: /Measurement/Details/5

        public ActionResult Details(int id = 0)
        {
            Measurement measurement = db.Measurements.Find(id);
            if (measurement == null)
            {
                return HttpNotFound();
            }
            return View(measurement);
        }

        //
        // GET: /Measurement/Create

        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username");
            return View();
        }

        //
        // POST: /Measurement/Create

        [HttpPost]
        public ActionResult Create(Measurement measurement)
        {
            if (ModelState.IsValid)
            {
                db.Measurements.Add(measurement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", measurement.UserId);
            return View(measurement);
        }

        //
        // GET: /Measurement/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Measurement measurement = db.Measurements.Find(id);
            if (measurement == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", measurement.UserId);
            return View(measurement);
        }

        //
        // POST: /Measurement/Edit/5

        [HttpPost]
        public ActionResult Edit(Measurement measurement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(measurement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", measurement.UserId);
            return View(measurement);
        }

        //
        // GET: /Measurement/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Measurement measurement = db.Measurements.Find(id);
            if (measurement == null)
            {
                return HttpNotFound();
            }
            return View(measurement);
        }

        //
        // POST: /Measurement/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Measurement measurement = db.Measurements.Find(id);
            db.Measurements.Remove(measurement);
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