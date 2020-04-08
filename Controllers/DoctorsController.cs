using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HSNHospitalProject.Models;
using HSNHospitalProject.Models.ViewModels;
using System.Diagnostics;
//for datetime 
using System.Globalization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;


namespace HSNHospitalProject.Controllers
{

    public class DoctorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Doctor
        public ActionResult List(string searchkey, int pagenum =0)
        {
            bool loggedIn = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if(loggedIn == true)
            {
                string query = "select * from doctors";
                List<SqlParameter> sqlparams = new List<SqlParameter>();
                if (searchkey != "")
                {
                    query = query + " where doctorFName like @searchkey";
                    sqlparams.Add(new SqlParameter("@searchkey", "%" + searchkey + "%"));

                }


                List<Doctors> doctor = db.Doctors.SqlQuery(query, sqlparams.ToArray()).ToList();
                //state of the page

                int perpage = 3;
                int docnumber = doctor.Count();
                int maxpage = (int)Math.Ceiling((decimal)docnumber / perpage) - 1;
                if (maxpage < 0) maxpage = 0;
                if (pagenum < 0) pagenum = 0;
                if (pagenum > maxpage) pagenum = maxpage;
                int start = (int)(perpage * pagenum);
                ViewData["pagenum"] = pagenum;
                ViewData["pagesummary"] = "";
                if (maxpage > 0)
                {
                    ViewData["pagesummary"] = (pagenum + 1) + " of " + (maxpage + 1);
                    List<SqlParameter> newparams = new List<SqlParameter>();

                    if (searchkey != "")
                    {
                        newparams.Add(new SqlParameter("@searchkey", "%" + searchkey + "%"));
                        ViewData["searchkey"] = searchkey;
                    }
                    newparams.Add(new SqlParameter("@start", start));
                    newparams.Add(new SqlParameter("@perpage", perpage));
                    string pagedquery = query + " order by doctorId offset @start rows fetch first @perpage rows only ";
                    doctor = db.Doctors.SqlQuery(pagedquery, newparams.ToArray()).ToList();
                }
                return View(doctor);

            }
            else
            {
                //if the user is not logged in then it take them to the register page
                return Redirect("https://localhost:44315/Account/Register");

            }




            //I am trying to fetch the data of department with the viewmodel 
            //so that I can use it for ajax search with department 
            //on the list page

            //    if (departmentId = null)
            //{ 
            //    DoctorDepartment viewmodel = new DoctorDepartment();
            //    viewmodel.departments = db.Departments.ToList();
            //    viewmodel.doctors = db.Doctors.ToList();
            //    return View(viewmodel);
            //}
            //else
            //{
            //    DoctorDepartment viewmodel = new DoctorDepartment();
            //    viewmodel.departments = db.Departments.ToList();
            //    string query = "select * from doctors where departmentId =@id";
            //    var Parameter = new SqlParameter("@id", departmentId);
            //    Doctors doctors = db.Doctors.SqlQuery(query, Parameter).FirstOrDefault();
            //    viewmodel.doctors = db.Doctors.ToList();
            //    return View(viewmodel);
            //}

        }
        

        // GET: Doctor/Details/5
        public ActionResult Show(int? id)
        {
            string user = User.Identity.GetUserId();
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctors doctors = db.Doctors.Find(id);
            if (doctors == null)
            {
                return HttpNotFound();
            }
            return View(doctors);
        }

        // GET: Doctor/Create
        public ActionResult Add()
        {
            DoctorDepartment viewmodel = new DoctorDepartment();
            viewmodel.departments = db.Departments.ToList();
            return View(viewmodel);
        }

        // POST: Doctor/Create
        [HttpPost]
        public ActionResult Add(string f_name, string l_name, string dob, string p_number, string e_address, string join_date, int departmentId)
        {
            bool admin = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId()).is_admin;
            if(admin == true)
            {
                string query = "insert into doctors (doctorFName , doctorLName, doctorDOB, doctorPNumber, doctorEAddress, doctorJoinDate, departmentID) values(@f_name, @l_name, @dob, @p_number,@e_address, @join_date, @departmentID)";
                SqlParameter[] sqlparams = new SqlParameter[7];
                sqlparams[0] = new SqlParameter("@f_name", f_name);
                sqlparams[1] = new SqlParameter("@l_name", l_name);
                sqlparams[2] = new SqlParameter("@dob", dob);
                sqlparams[3] = new SqlParameter("@p_number", p_number);
                sqlparams[4] = new SqlParameter("@e_address", e_address);
                sqlparams[5] = new SqlParameter("@join_date", join_date);
                sqlparams[6] = new SqlParameter("@departmentId", departmentId);

                db.Database.ExecuteSqlCommand(query, sqlparams);
                return RedirectToAction("List");

            }
            else
            {
                return Redirect("https://localhost:44315/Error/Index");

            }


        }

        // GET: Doctor/Edit/5
        public ActionResult Update(string id)
        {

            bool admin = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId()).is_admin;
            if(admin == true)
            {
                string query = "select * from doctors where doctorId =@id";
                var Parameter = new SqlParameter("@id", id);
                Doctors doctors = db.Doctors.SqlQuery(query, Parameter).FirstOrDefault();

                List<Department> departments = db.Departments.SqlQuery("select * from departments").ToList();
                DoctorDepartment viewmodel = new DoctorDepartment();
                viewmodel.departments = departments;
                viewmodel.Doctors = doctors;
                return View(viewmodel);
                

            }
            else
            {
                return Redirect("https://localhost:44315/Error/Index");

            }

        }

        // POST: Doctor/Edit/5
        [HttpPost]
        public ActionResult Update(string id, string f_name, string l_name, string dob, string p_number, string e_address, string join_date, int departmentId)
        {
            bool admin = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId()).is_admin;
            if(admin == true)
            {
                string query = "Update doctors set doctorFName = @f_name, doctorLName=@l_name, doctorDOB =@dob, doctorPNumber=@p_number, doctorEAddress = @e_address,doctorJoinDate = @join_date, departmentId=@departmentId where doctorId =@id";
                SqlParameter[] sqlparams = new SqlParameter[8];
                sqlparams[0] = new SqlParameter("@f_name", f_name);
                sqlparams[1] = new SqlParameter("@l_name", l_name);
                sqlparams[2] = new SqlParameter("@dob", dob);
                sqlparams[3] = new SqlParameter("@p_number", p_number);
                sqlparams[4] = new SqlParameter("@e_address", e_address);
                sqlparams[5] = new SqlParameter("@join_date", join_date);
                sqlparams[6] = new SqlParameter("@departmentId", departmentId);
                sqlparams[7] = new SqlParameter("@id", id);

                db.Database.ExecuteSqlCommand(query, sqlparams);
                return RedirectToAction("List");

            }
            else
            {
                return Redirect("https://localhost:44315/Error/Index");

            }


            
        }

        // GET: Doctor/Delete/5
        //public ActionResult DeleteConfirm(string id)
        //{
        //    string query = "select * from doctors where doctorId = @id";
        //    SqlParameter param = new SqlParameter("@id", id);
        //    Doctors doctors = db.Doctors.SqlQuery(query, param).FirstOrDefault();
        //    return View(doctors);
        //}

        // POST: Doctor/Delete/5
        public ActionResult Delete(string id)
        {
            bool admin = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId()).is_admin;
            if(admin == true)
            {
                string query = "delete from doctors where doctorId=@id";
                SqlParameter param = new SqlParameter("@id", id);
                db.Database.ExecuteSqlCommand(query, param);
                return RedirectToAction("List");
            }
            else
            {
                return Redirect("https://localhost:44315/Error/Index");
            
            }
            
        }
    }
}
