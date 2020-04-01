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
    public class JobPostController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: JobPost
        public ActionResult Index()
        {
            return View(db.JobPosts.ToList());
        }

        // GET: JobPost/Details/8
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Debug Purpose to see if we are getting the id
            Debug.WriteLine("I'm pulling data of " + id.ToString());

            //Get the specific JobPost
            JobPost jobPost = db.JobPosts.Find(id);

            //Could not find the specific JobPost
            if (jobPost == null)
            {
                return HttpNotFound();
            }

            //return the JobPost data 
            return View(jobPost);
        }

        // GET: JobPost/Create
        public ActionResult Create()
        {
            JobPostCreateViewModel jobPostCreateViewModel = new JobPostCreateViewModel();

            //Method for creating the drop down list is referenced from: 
            //https://stackoverflow.com/questions/41512619/how-to-create-asp-net-mvc-dropdownlist-with-required-validation
            jobPostCreateViewModel.allDepartments = GetAllDepartments();
            return View(jobPostCreateViewModel);
        }

        //When user submits the form to add a new JobPost
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(JobPostCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Debug Purpose to see if we are getting the data
                Debug.WriteLine("I'm pulling data of Name: " + model.name + ", Type: " + model.type + ", DepartmentId: " + model.departmentId
                    + ", Experience: " + model.experience + ", Salary: " + model.salary + ", Content: " + model.content);

                /*
                //Query Method (Does not work for the closed date error)
                
                //The query to add a new JobPost
                string query = "INSERT INTO JobPosts " +
                    "(jobPostType, jobPostName, departmentId, jobPostExperience, jobPostFilled, jobPostSalary, jobPostPostedDate, jobPostClosedDate, jobPostContent) " +
                    "VALUES " +
                    "(@jobPostType, @jobPostName, @departmentId, @jobPostExperience, @jobPostFilled, @jobPostSalary, @jobPostPostedDate, @jobPostClosedDate, @jobPostContent)";

                SqlParameter[] sqlparams = new SqlParameter[9];
                
                //Set the JobPost Type Depending on the enum
                if (model.type == JobType.Job) {
                    sqlparams[0] = new SqlParameter("@jobPostType", "Job");
                }
                else if (model.type == JobType.Volunteer) {
                    sqlparams[0] = new SqlParameter("@jobPostType", "Volunteer");
                }
                //Something went wrong (should not happen)
                else {
                    model.allDepartments = GetAllDepartments();
                    return View(model);
                }

                sqlparams[1] = new SqlParameter("@jobPostName", model.name);
                sqlparams[2] = new SqlParameter("@departmentId", model.departmentId);
                sqlparams[3] = new SqlParameter("@jobPostExperience", model.experience);
                sqlparams[4] = new SqlParameter("@jobPostFilled", false);
                sqlparams[5] = new SqlParameter("@jobPostSalary", model.salary);
                sqlparams[6] = new SqlParameter("@jobPostPostedDate", DateTime.Now);
                sqlparams[7] = new SqlParameter("@jobPostClosedDate", null);
                sqlparams[8] = new SqlParameter("@jobPostContent", model.Content);

                //Run the sql command
                db.Database.ExecuteSqlCommand(query, sqlparams);
                */

                //Using object to add a new JobPost
                JobPost newJobPost = new JobPost();
                //Set the JobPost Type Depending on the enum
                if (model.type == JobType.Job)
                {
                    newJobPost.jobPostType = "Job";
                }
                else if (model.type == JobType.Volunteer)
                {
                    newJobPost.jobPostType = "Volunteer";
                }
                //Something went wrong (should not happen)
                else
                {
                    model.allDepartments = GetAllDepartments();
                    return View(model);
                }
                newJobPost.jobPostName = model.name;
                newJobPost.departmentId = model.departmentId;
                newJobPost.jobPostExperience = model.experience;
                newJobPost.jobPostFilled = false;
                newJobPost.jobPostSalary = model.salary;
                newJobPost.jobPostPostedDate = DateTime.Now;
                newJobPost.jobPostClosedDate = null;
                newJobPost.jobPostContent = model.content;

                //Add the object to the database
                db.JobPosts.Add(newJobPost);
                
                //Save the changes in the database
                db.SaveChanges();

                //Go back to the list of JobPost to see the added JobPost
                return RedirectToAction("Index");
            }

            //Something failed, redisplay form
            model.allDepartments = GetAllDepartments();
            return View(model);
        }

        // GET: JobPost/Edit/8
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //change to redirect to 
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Debug Purpose to see if we are getting the id
            Debug.WriteLine("I'm pulling data of " + id.ToString());

            //Get the specific jobPost
            JobPost jobPost = db.JobPosts.Find(id);

            //Could not find the specific jobPost
            if (jobPost == null)
            {
                return HttpNotFound();
            }

            //Must create a view model of the jobPost data
            JobPostEditViewModel jobPostEditViewModel = new JobPostEditViewModel();
            jobPostEditViewModel.id = jobPost.jobPostId;
            jobPostEditViewModel.name = jobPost.jobPostName;
            if (jobPost.jobPostType == JobType.Job.ToString()) {
                jobPostEditViewModel.type = JobType.Job;
            }
            else if (jobPost.jobPostType == JobType.Volunteer.ToString()) {
                jobPostEditViewModel.type = JobType.Volunteer;
            }

            jobPostEditViewModel.departmentId = jobPost.departmentId;
            jobPostEditViewModel.allDepartments = GetAllDepartments();
            jobPostEditViewModel.experience = jobPost.jobPostExperience;
            jobPostEditViewModel.filled = jobPost.jobPostFilled;
            jobPostEditViewModel.salary = jobPost.jobPostSalary;
            jobPostEditViewModel.postedDate = jobPost.jobPostPostedDate;
            jobPostEditViewModel.closedDate = jobPost.jobPostClosedDate;
            jobPostEditViewModel.content = jobPost.jobPostContent;

            //return the jobPost data 
            return View(jobPostEditViewModel);
        }

        //When user submits the form to edit a JobPost
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(JobPostEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Debug Purpose to see if we are getting the data
                Debug.WriteLine("I'm pulling data of id: " + model.id + "Name: " + model.name + ", Type: " + model.type + ", DepartmentId: " + model.departmentId
                    + ", Experience: " + model.experience + ", Filled: " + model.filled +  ", Salary: " + model.salary
                    + ", Posted Date: " + model.postedDate + ", Closed Date: " + model.closedDate + ", Content: " + model.content);

                //Using object to edit a JobPost
                JobPost jobPost = db.JobPosts.Find(model.id);
                //Set the JobPost Type Depending on the enum
                if (model.type == JobType.Job)
                {
                    jobPost.jobPostType = "Job";
                }
                else if (model.type == JobType.Volunteer)
                {
                    jobPost.jobPostType = "Volunteer";
                }
                //Something went wrong (should not happen)
                else
                {
                    model.allDepartments = GetAllDepartments();
                    return View(model);
                }
                jobPost.jobPostName = model.name;
                jobPost.departmentId = model.departmentId;
                jobPost.jobPostExperience = model.experience;
                jobPost.jobPostFilled = model.filled;
                jobPost.jobPostSalary = model.salary;
                jobPost.jobPostPostedDate = model.postedDate;
                if (model.filled)
                {
                    jobPost.jobPostClosedDate = DateTime.Now;
                }
                else
                {
                    jobPost.jobPostClosedDate = null;
                }

                jobPost.jobPostContent = model.content;

                //Save the changes in the database
                db.SaveChanges();

                //Go back to the list of JobPost to see the added JobPost
                return RedirectToAction("Index");
            }

            //Something failed, redisplay form
            model.allDepartments = GetAllDepartments();
            return View(model);
        }

        // GET: JobPost/Delete/8
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Debug Purpose to see if we are getting the id
            Debug.WriteLine("I'm pulling data of " + id.ToString());

            //Get the specific JobPost
            JobPost jobPost = db.JobPosts.Find(id);

            //Could not find the specific JobPost
            if (jobPost == null)
            {
                return HttpNotFound();
            }

            //return the JobPost data 
            return View(jobPost);
        }

        // POST: JobPost/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Debug Purpose to see if we are getting the id
            Debug.WriteLine("I'm pulling data of " + id.ToString());

            //Get the specific JobPost
            JobPost jobPost = db.JobPosts.Find(id);

            //Delete that specific JobPost from the database
            db.JobPosts.Remove(jobPost);

            //Save the changes on the database
            db.SaveChanges();

            //Go back to the list of JobPost to see the removed JobPost
            return RedirectToAction("Index");
        }

        public List<SelectListItem> GetAllDepartments()
        {
            List<SelectListItem> allDepartments = db.Departments.Select(x => new SelectListItem { Text = x.departmentName, Value = x.departmentId.ToString() })
            .ToList();
            allDepartments.Insert(0, new SelectListItem { Text = "Choose a Department", Value = "" });
            return allDepartments;
        }

    }
}