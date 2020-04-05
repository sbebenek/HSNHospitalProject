using System;
using System.Collections.Generic;
using System.Data;
//required for SqlParameter class
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HSNHospitalProject.Models;
using System.Diagnostics;
//needed for await
using System.Threading.Tasks;
//needed for other sign in feature classes
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
//view model
using HSNHospitalProject.Models.ViewModels;


namespace HSNHospitalProject.Controllers
{
    public class PatientsController : Controller
    {
        //patient controller only here to store the information of the patient
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        //as patient are users only

        private ApplicationDbContext db = new ApplicationDbContext();

        public PatientsController() { }
        // GET: Patients

        public ActionResult List()
        {


            //display only the booked appointment to booked user
            string id = User.Identity.GetUserId();
            Debug.WriteLine(id);
            //string query = "select * from appointments where patientId ="+patientId;

            //List<Appointments> appointments = db.Appointments.SqlQuery(query).ToList();

            List<Patients> patients;
            patients = db.Patients.Where(p => p.patientId.Contains(id)).ToList();


            return View(patients);

        }

        // GET: Patients/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Patients/Create
        public ActionResult Add()
        {
            AddAppointment addappointment = new AddAppointment();
            addappointment.Doctors = db.Doctors.ToList();
            return View(addappointment);
        }

        // POST: Patients/Create
        [HttpPost]
        public ActionResult Add(string f_name, string l_name, string dob, string user_name, string password, string p_number, string e_address, string address, string health_number)
        {
            //store logged in userid in a string
            //by this way we can eliminate the duplicate entry of a patient
            string patientId = User.Identity.GetUserId();

            Debug.WriteLine(patientId);
            Patients newpatients = new Patients();
            newpatients.patientId = patientId;
            newpatients.patientFName = f_name;
            newpatients.patientLName = l_name;
            newpatients.patientDOB = dob;
            newpatients.patientPNumber = p_number;
            newpatients.patientEAddress = e_address;
            newpatients.patientHAddress = address;
            newpatients.patientHealthCard = health_number;
            db.Patients.Add(newpatients);
            db.SaveChanges();
            return RedirectToAction("List");


        }

        // GET: Patients/Edit/5
        public ActionResult Edit(string id)
        {

            Patients patients = db.Patients.Find(id);

            return View(patients);

        }

        // POST: Patients/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, string f_name, string l_name, string dob, string p_number, string e_address, string address, string health_number)
        {
            string query = "Update patients set patientFName = @f_name, patientLName=@l_name, patientDOB =@dob, patientPNumber=@p_number, patientEAddress = @e_address, patientHAddress=@address, patientHealthCard=@health_number where patientId =@id";
            SqlParameter[] sqlparams = new SqlParameter[8];
            sqlparams[0] = new SqlParameter("@f_name", f_name);
            sqlparams[1] = new SqlParameter("@l_name", l_name);
            sqlparams[2] = new SqlParameter("@dob", dob);
            sqlparams[3] = new SqlParameter("@p_number", p_number);
            sqlparams[4] = new SqlParameter("@e_address", e_address);
            sqlparams[5] = new SqlParameter("@address", address);
            sqlparams[6] = new SqlParameter("@health_number", health_number);
            sqlparams[7] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");

        }

        // GET: Patients/Delete/5
        public ActionResult Delete(string id)
        {
            string query = "delete from patients where patientId = @id";
            SqlParameter[] sqlparams = new SqlParameter[1];
            sqlparams[0] = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("List");

        }

        // POST: Patients/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
