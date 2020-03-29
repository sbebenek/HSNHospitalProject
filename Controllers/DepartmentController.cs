using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Data.SqlClient;
using HSNHospitalProject.Models;
using HSNHospitalProject.Models.ViewModels;

namespace HSNHospitalProject.Controllers
{
    public class DepartmentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Department
        public ActionResult Index()
        {
            return View(db.Departments.ToList());
        }

        // GET: Department/Details/8
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Department/Create
        public ActionResult Create()
        {
            return View();
        }

        //When user submits the form to add a new Department
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DepartmentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Debug Purpose to see if we are getting the data
                Debug.WriteLine("I'm pulling data of " + model.name);

                if (model.name != "") {
                    //The query to add a new Department
                    string query = "INSERT INTO Departments (departmentName) VALUES (@departmentName)";
                    SqlParameter sqlparam = new SqlParameter("@departmentName", model.name);

                    //Run the sql command
                    db.Database.ExecuteSqlCommand(query, sqlparam);

                    //Go back to the list of Department to see the added Department
                    return RedirectToAction("Index");
                }               
            }

            //Something failed, redisplay form
            return View(model);
        }


    }
}