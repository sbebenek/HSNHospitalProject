using HSNHospitalProject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HSNHospitalProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// This action calculates the average rating of each weekday, passes it to the partial view that
        /// contains a js function that draws the graph, and returns the _StartGraph partial view
        /// </summary>
        [HttpPost]
        public ActionResult startGraph()
        {
            Debug.WriteLine("The AJAX call to start generating the activity graph has been received. Generating...");

            Debug.WriteLine("Getting database connection...");
            ApplicationDbContext db = new ApplicationDbContext();
            Debug.WriteLine("Success.");

            //grabbing all activity records
            Debug.WriteLine("Grabbing all activity records...");
            List<ActivityRecords> records = db.ActivityRecords.ToList();
            Debug.WriteLine("Success.");


            //returns a partial view with the script that will draw the graph
            return PartialView("_StartGraph");
        }
    }
}