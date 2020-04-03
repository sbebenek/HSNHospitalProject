using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HSNHospitalProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PagedList;


namespace HSNHospitalProject.Controllers
{
    public class ActivityRecordsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ActivityRecords
        public ActionResult Index(int? page)
        {
            List<ActivityRecords> records = db.ActivityRecords.ToList();

            //check if the user is logged in (true if logged in)
            bool isLoggedIn = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            //is admin is false by default
            bool isAdmin = false;
            //if the user is logged in, isAdmin = whether or not the user is an admin
            if (isLoggedIn)
            {
                //below custom column check from https://stackoverflow.com/questions/31864400/how-get-custom-field-in-aspnetusers-table
                isAdmin = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId()).is_admin;
            }
            if(!isAdmin)
            {

            }

            //the amount of records shown per page
            int pageSize = 20;
            //set the page number to 1 if it is not already set
            int pageNumber = (page ?? 1);

            return View(records.ToPagedList(pageNumber, pageSize));
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
            ViewBag.date = DateTime.Now.ToString("yyyy-MM-dd");
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

        //********DELETING OLDER THAN X MONTHS*******
        //https://forums.asp.net/t/1218330.aspx?If+date+is+older+than+today+then+delete+record+
        // DELETE FROM <your table name> WHERE  DateField < GETDATE()
        //https://stackoverflow.com/questions/10978520/howto-asp-net-sql-query-where-datetime-greater-than
        //WHERE PublishDate >= Getdate() -7



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
