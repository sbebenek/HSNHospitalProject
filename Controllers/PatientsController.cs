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
using HSNHospitalProject.Logs;


namespace HSNHospitalProject.Controllers
{
    public class PatientsController : Controller
    {
        //patient controller only here to store the information of the patient
        //as patient are users only

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Patients

        public ActionResult List()
        {

            if (LoggedIn.isLoggedIn() && !LoggedIn.isAdmin())
            {
                //check the logged in and dispaly only that information which related to that person
                string id = User.Identity.GetUserId();
                Debug.WriteLine(id);
                string query = "select * from Patients where patientId = @id";
                var Parameter = new SqlParameter("@id", id);
                List<Patients> patients = db.Patients.SqlQuery(query, Parameter).ToList();
                AddAppointment viewmodel = new AddAppointment();
                viewmodel.Patients = patients;

                if (patients == null)
                {
                    return RedirectToAction("Add", "Patients");
                }
                return View(viewmodel);


            }
            if (LoggedIn.isLoggedIn() && LoggedIn.isAdmin())
            {
                List<Patients> patients = db.Patients.ToList();
                AddAppointment viewmodel = new AddAppointment();
                viewmodel.Patients = patients;
                if (patients == null)
                {
                    return RedirectToAction("Add");
                }
                return View(viewmodel);

            }
            return RedirectToAction("Login", "Account");



        }

        // GET: Patients/Details/5
        public ActionResult Show(string id)
        {
            if (LoggedIn.isLoggedIn())
            {
                string query = "select * from Appointments where patientId = @id ORDER BY appointmentDate";
                var Parameter = new SqlParameter("@id", id);
                List<Appointments> appointments = db.Appointments.SqlQuery(query, Parameter).ToList();
                Patients patients = db.Patients.Find(id);
                AddAppointment viewmodel = new AddAppointment();
                viewmodel.Appointments = appointments;
                viewmodel.Patient = patients;
                return View(viewmodel);
            }
            return View("List");
        }

        // GET: Patients/Create
        public ActionResult Add()
        {
            if (LoggedIn.isLoggedIn())
            {
                AddAppointment addappointment = new AddAppointment();
                addappointment.Doctors = db.Doctors.ToList();
                return View(addappointment);
            }
            return RedirectToAction("Login", "AccountController");

        }

        // POST: Patients/Create
        [HttpPost]
        public ActionResult Add(string f_name, string l_name, string dob, string p_number, string e_address, string address, string health_number)
        {
            if (LoggedIn.isLoggedIn())
            {
                //store logged in userid in a string
                //by this way we can eliminate the duplicate entry of a patient
                string patientId = User.Identity.GetUserId();
                //get the value of the userid which is store in the patient table
                string query = "select patientId from patients";
                //Patients patients = db.Patients.SqlQuery(query).FirstOrDefault();
                //convert into the string so that we can compare user id to the logged in user id
                //if there is already a entry from logged in user 
                //he/she will not allow to make a duplicate entry
                //string p = patients.ToString();
                //if (patientId == p)
                //{
                //    return RedirectToAction("Index", "Error");
                //}

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
            
            return RedirectToAction("Login", "AccountController");


        }

        // GET: Patients/Edit/5
        public ActionResult Edit(string id)
        {
            if (LoggedIn.isLoggedIn())
            {
                Patients patients = db.Patients.Find(id);

                return View(patients);
            }
            return RedirectToAction("Login", "AccountController");

        }

        // POST: Patients/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, string f_name, string l_name, string dob, string p_number, string e_address, string address, string health_number)
        {
            if (LoggedIn.isLoggedIn())
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
            return RedirectToAction("Login", "AccountController");



        }

        // GET: Patients/Delete/5
        public ActionResult Delete(string id)
        {
            if (LoggedIn.isLoggedIn())
            {
                string query1 = "select * from appointments where patientId = @id";
                var Parameter = new SqlParameter("@id", id);
                Appointments appointments = db.Appointments.SqlQuery(query1, Parameter).FirstOrDefault();

                if (appointments != null)
                {
                    return RedirectToAction("Index", "Error");
                }
                string query = "delete from patients where patientId = @id";
                SqlParameter[] sqlparams = new SqlParameter[1];
                sqlparams[0] = new SqlParameter("@id", id);
                db.Database.ExecuteSqlCommand(query, sqlparams);

                return RedirectToAction("List");

            }
            return RedirectToAction("Login", "AccountController");


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
