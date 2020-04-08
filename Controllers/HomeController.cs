using HSNHospitalProject.Helpers;
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
            //only grabs from the db once upon home page load, then parses each day from that list
            Debug.WriteLine("Grabbing all activity records...");
            List<ActivityRecords> records = db.ActivityRecords.ToList();
            Debug.WriteLine("Success.");

            //grabbing all Sunday records
            List<ActivityRecords> sundayRecords = records.Where(r => r.activityrecorddate.DayOfWeek == DayOfWeek.Sunday).ToList();
            int averageSunRating = AverageRating(sundayRecords);
            Debug.WriteLine("Number of records for Sunday: " + sundayRecords.Count + ". Average rating: " + averageSunRating);

            //grabbing all monday records
            List<ActivityRecords> mondayRecords = records.Where(r => r.activityrecorddate.DayOfWeek == DayOfWeek.Monday).ToList();
            int averageMonRating = AverageRating(mondayRecords);
            Debug.WriteLine("Number of records for Monday: " + mondayRecords.Count + ". Average rating: " + averageMonRating);

            //grabbing all Tuesday records
            List<ActivityRecords> tuesdayRecords = records.Where(r => r.activityrecorddate.DayOfWeek == DayOfWeek.Tuesday).ToList();
            int averageTuesRating = AverageRating(tuesdayRecords);
            Debug.WriteLine("Number of records for Tuesday: " + tuesdayRecords.Count + ". Average rating: " + averageTuesRating);

            //grabbing all Wednesday records
            List<ActivityRecords> wednesdayRecords = records.Where(r => r.activityrecorddate.DayOfWeek == DayOfWeek.Wednesday).ToList();
            int averageWedRating = AverageRating(wednesdayRecords);
            Debug.WriteLine("Number of records for Wednesday: " + wednesdayRecords.Count + ". Average rating: " + averageWedRating);

            //grabbing all Thursday records
            List<ActivityRecords> thursdayRecords = records.Where(r => r.activityrecorddate.DayOfWeek == DayOfWeek.Thursday).ToList();
            int averageThursRating = AverageRating(thursdayRecords);
            Debug.WriteLine("Number of records for Thursday: " + thursdayRecords.Count + ". Average rating: " + averageThursRating);

            //grabbing all Friday records
            List<ActivityRecords> fridayRecords = records.Where(r => r.activityrecorddate.DayOfWeek == DayOfWeek.Friday).ToList();
            int averageFriRating = AverageRating(fridayRecords);
            Debug.WriteLine("Number of records for Friday: " + fridayRecords.Count + ". Average rating: " + averageFriRating);

            //grabbing all Saturday records
            List<ActivityRecords> saturdayRecords = records.Where(r => r.activityrecorddate.DayOfWeek == DayOfWeek.Saturday).ToList();
            int averageSatRating = AverageRating(saturdayRecords);
            Debug.WriteLine("Number of records for Saturday: " + saturdayRecords.Count + ". Average rating: " + averageSatRating);

            //The canvas on the home index page has the id="graph"
            GraphValueHolder valueHolder = new GraphValueHolder("graph", averageSunRating, averageMonRating, averageTuesRating, averageWedRating, averageThursRating,
                averageFriRating, averageSatRating);

            Debug.WriteLine("Average weekday values passed to GraphGenerator.js method, will print to <canvas> element with id=\"graph\"");
            //returns a partial view with the script that will draw the graph
            return PartialView("_StartGraph", valueHolder);
        }

        /// <summary>
        /// This method takes a List of ActivityRecords objects and returns the average rating.
        /// </summary>
        /// <param name="records">A list of activity records.</param>
        /// <returns>An int average rating value.</returns>
        private int AverageRating(List<ActivityRecords> records)
        {
            //if there are no records with this date, return the lowest value (1)
            if (records == null || records.Count == 0)
            {
                return 1;
            }

            int sum = 0;
            foreach (ActivityRecords record in records)
            {
                sum += record.activityrecordrating;
            }

            int average = sum / records.Count;
            return average;
        }
    }
}