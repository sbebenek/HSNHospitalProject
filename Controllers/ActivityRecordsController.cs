using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HSNHospitalProject.Models;

namespace HSNHospitalProject.Controllers
{
    public class ActivityRecordsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ActivityRecords
        public ActionResult Index()
        {
            return View(db.ActivityRecords.ToList());
        }

        // GET: ActivityRecords/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityRecords activityRecords = db.ActivityRecords.Find(id);
            if (activityRecords == null)
            {
                return HttpNotFound();
            }
            return View(activityRecords);
        }

        // GET: ActivityRecords/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActivityRecords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "activityrecordid,activityrecorddate,activityrecordrating")] ActivityRecords activityRecords)
        {
            if (ModelState.IsValid)
            {
                db.ActivityRecords.Add(activityRecords);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(activityRecords);
        }

        // GET: ActivityRecords/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityRecords activityRecords = db.ActivityRecords.Find(id);
            if (activityRecords == null)
            {
                return HttpNotFound();
            }
            return View(activityRecords);
        }

        // POST: ActivityRecords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "activityrecordid,activityrecorddate,activityrecordrating")] ActivityRecords activityRecords)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activityRecords).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(activityRecords);
        }

        // GET: ActivityRecords/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityRecords activityRecords = db.ActivityRecords.Find(id);
            if (activityRecords == null)
            {
                return HttpNotFound();
            }
            return View(activityRecords);
        }

        // POST: ActivityRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ActivityRecords activityRecords = db.ActivityRecords.Find(id);
            db.ActivityRecords.Remove(activityRecords);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
