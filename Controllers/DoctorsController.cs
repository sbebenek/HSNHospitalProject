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
        public ActionResult List()
        {

            return View(db.Doctors.ToList());
        }

        // GET: Doctor/Details/5
        public ActionResult Show(int? id)
        {
            string user = User.Identity.GetUserId();
            //if(user.is_admin = true)
            //{

            //}
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

        // GET: Doctor/Edit/5
        public ActionResult Update(string id)
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

        // POST: Doctor/Edit/5
        [HttpPost]
        public ActionResult Update(string id, string f_name, string l_name, string dob, string p_number, string e_address, string join_date, int departmentId)
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

        // GET: Doctor/Delete/5
        public ActionResult DeleteConfirm(string id)
        {
            string query = "select * from doctors where doctorId = @id";
            SqlParameter param = new SqlParameter("@id", id);
            Doctors doctors = db.Doctors.SqlQuery(query, param).FirstOrDefault();
            return View(doctors);
        }

        // POST: Doctor/Delete/5
        [HttpPost]
        public ActionResult Delete(string id)
        {
            string query = "delete from doctors where doctorId=@id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);
            return RedirectToAction("List");
        }
    }
}
