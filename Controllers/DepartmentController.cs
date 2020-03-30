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

            //Debug Purpose to see if we are getting the id
            Debug.WriteLine("I'm pulling data of " + id.ToString());

            //Get the specific department
            Department department = db.Departments.Find(id);

            //Could not find the specific department
            if (department == null)
            {
                return HttpNotFound();
            }

            //return the department data 
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

        // GET: Department/Edit/8
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //change to redirect to 
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Debug Purpose to see if we are getting the id
            Debug.WriteLine("I'm pulling data of " + id.ToString());

            //Get the specific department
            Department department = db.Departments.Find(id);

            //Could not find the specific department
            if (department == null)
            {
                return HttpNotFound();
            }

            //Must create a view model of the department data
            DepartmentEditViewModel departmentEditViewModel = new DepartmentEditViewModel();
            departmentEditViewModel.id = department.departmentId;
            departmentEditViewModel.name = department.departmentName;

            //return the department data 
            return View(departmentEditViewModel);
        }

        //When user submits the form to add a new Department
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DepartmentEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Debug Purpose to see if we are getting the data
                Debug.WriteLine("I'm pulling data of " + model.name);

                if (model.name != "")
                {
                    //The query to update a new Department
                    string query = "UPDATE Departments SET departmentName = @departmentName WHERE departmentId = @departmentId";
                    SqlParameter[] sqlparams = new SqlParameter[2];
                    sqlparams[0] = new SqlParameter("@departmentName", model.name);
                    sqlparams[1] = new SqlParameter("@departmentId", model.id);

                    //Run the sql command
                    db.Database.ExecuteSqlCommand(query, sqlparams);

                    //Go back to the list of Department to see the edited Department
                    return RedirectToAction("Index");
                }
            }

            //Something failed, redisplay form
            return View(model);
        }

        // GET: Department/Delete/8
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Debug Purpose to see if we are getting the id
            Debug.WriteLine("I'm pulling data of " + id.ToString());

            //Get the specific department
            Department department = db.Departments.Find(id);

            //Could not find the specific department
            if (department == null)
            {
                return HttpNotFound();
            }

            //return the department data 
            return View(department);
        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Debug Purpose to see if we are getting the id
            Debug.WriteLine("I'm pulling data of " + id.ToString());

            //Get the specific department
            Department department = db.Departments.Find(id);

            //Delete that specific department from the database
            db.Departments.Remove(department);

            //Save the changes on the database
            db.SaveChanges();

            //Go back to the list of Department to see the removed Department
            return RedirectToAction("Index");
        }
    }
}