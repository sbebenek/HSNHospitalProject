using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
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
            records.Sort((x, y) => DateTime.Compare(y.activityrecorddate, x.activityrecorddate));

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
            if (!isAdmin)
            {
                //redirect to login page if not a logged in admin
                //return RedirectToAction("Login", "AccountController");
            }

            //the amount of records shown per page
            int pageSize = 10;
            //set the page number to 1 if it is not already set
            int pageNumber = (page ?? 1);

            return View(records.ToPagedList(pageNumber, pageSize));
        }

        // GET: ActivityRecords/Details/5
        /*public ActionResult Details(int? id)
        {
            //DETAILS PAGE WILL NOT BE USED. REMOVE AT SOME POINT
        }*/

        // GET: ActivityRecords/Create
        public ActionResult Create()
        {
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
            if (!isAdmin)
            {
                //redirect to login page if not a logged in admin
                //return RedirectToAction("Login", "AccountController");
            }

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
            //******TODO: DON"T ADD IF A RECORD AT THAT DATE ALREADY EXISTS
            if (ModelState.IsValid)
            {
                //checking to see if a record already exists with this date
                ActivityRecords existingRecord = db.ActivityRecords.Where(record => record.activityrecorddate == activityRecords.activityrecorddate).FirstOrDefault();

                //if a record with that date already exists
                if (existingRecord != null)
                {
                    Debug.WriteLine("An activity record with the date you are trying to add already exists: " + existingRecord.ToString());
                    ViewBag.existingDate = existingRecord.activityrecorddate.ToLongDateString();
                    ViewBag.existingId = existingRecord.activityrecordid;
                    //return to the view and indicate a record with that date already exists, and prompt if the user wants to update that existing record instead
                    return View();
                }

                //else, a record with that date
                Debug.WriteLine("A record with the date you are trying to add does not yet exist. Adding it to the database...");
                db.ActivityRecords.Add(activityRecords);
                db.SaveChanges();
                return RedirectToAction("Index", new { add = true });
            }


            return View(activityRecords);
        }

        // GET: ActivityRecords/Edit/5
        public ActionResult Edit(int? id)
        {
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
            if (!isAdmin)
            {
                //redirect to login page if not a logged in admin
                //return RedirectToAction("Login", "AccountController");
            }

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
                return RedirectToAction("Index", new { update = true });
            }
            return View(activityRecords);
        }

        // GET: ActivityRecords/Delete/5
        public ActionResult Delete(int? id)
        {
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
            if (!isAdmin)
            {
                //redirect to login page if not a logged in admin
                //return RedirectToAction("Login", "AccountController");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityRecords activityRecords = db.ActivityRecords.Find(id);
            if (activityRecords == null)
            {
                return HttpNotFound();
            }
            db.ActivityRecords.Remove(activityRecords);
            db.SaveChanges();
            return RedirectToAction("Index", new { delete = true });
        }

        // POST: ActivityRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            /* ActivityRecords activityRecords = db.ActivityRecords.Find(id);
             db.ActivityRecords.Remove(activityRecords);
             db.SaveChanges();*/
            // a delete form shouldnt normally submit so this shouldn't run but in case it does
            Debug.WriteLine("The Delete Post form submitted! but how?");
            return RedirectToAction("Index");
        }

        //********DELETING OLDER THAN X MONTHS*******
        //https://forums.asp.net/t/1218330.aspx?If+date+is+older+than+today+then+delete+record+
        // DELETE FROM <your table name> WHERE  DateField < GETDATE()
        //https://stackoverflow.com/questions/10978520/howto-asp-net-sql-query-where-datetime-greater-than
        //WHERE PublishDate >= Getdate() -7
        [HttpGet]
        public ActionResult DeleteMultiple(int? numberOfMonths)
        {
            //if the form was submitted empty or with a negative number
            if (numberOfMonths == null || numberOfMonths < 0)
            {
                ViewBag.errorMessage = "Required field. Must be 0 or greater.";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.numMonths = numberOfMonths;
                DateTime deleteDate = DateTime.Today.AddMonths(0 - (int)numberOfMonths);
                ViewBag.deleteDate = deleteDate.ToLongDateString();
                ViewBag.deleteCount = db.ActivityRecords.Where(record => record.activityrecorddate < deleteDate).Count();
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMultiple(bool confirm, int numberOfMonths)
        {
            //TODO: finish deleteing where all records all older than numberOfMonths
            if(confirm == true)
            {
                DateTime deleteDate = DateTime.Today.AddMonths(0 - (int)numberOfMonths);
                List<ActivityRecords> recordsToBeDeleted = db.ActivityRecords.Where(record => record.activityrecorddate < deleteDate).ToList();
                foreach (var record in recordsToBeDeleted)
                {
                    //remove each row one by one
                    db.ActivityRecords.Remove(record);
                }
                try
                {
                    //save changes in the database
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Something went wrong when trying to remove all records older than " + deleteDate.ToLongDateString() + "from the database.");
                    Console.WriteLine(e);
                }
                return RedirectToAction("Index", new { delete = true });
            }
            else return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult showDeleteInfo(int activityrecordsid)
        {

            Debug.WriteLine("Receiving activityrecordsid in the ajax call as " + activityrecordsid);
            //grabbing the activity records object
            ActivityRecords activityRecords = db.ActivityRecords.Find(activityrecordsid);
            //returns a partial view with the given galleryImage and prints it back to whichever div the jquery call indicated in the view
            //(in this case, it will be inside a modal window)
            return PartialView("_ShowDeleteInfo", activityRecords);
        }
    }
}
