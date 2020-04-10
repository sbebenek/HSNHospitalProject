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
using HSNHospitalProject.Logs;

namespace HSNHospitalProject.Controllers
{
    public class AppointmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Appointments
        public ActionResult List()
        {

            //if a user logged in he/she can only see the booking which booked by them
            if (LoggedIn.isLoggedIn() && !LoggedIn.isAdmin())
            {
                string id = User.Identity.GetUserId();
                Debug.WriteLine(id);

                List<Appointments> appointments;
                appointments = db.Appointments.Where(a => a.patientId.Contains(id)).ToList();

                return View(appointments);

            }
            //if a admin is logged in ,can see every appointment
            if (LoggedIn.isLoggedIn() && LoggedIn.isAdmin())
            {
                List<Appointments> appointments = db.Appointments.OrderBy(a => a.appointmentDate).ToList();
                return View(appointments);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }


        }

        // GET: Appointments/Details/5
        public ActionResult Show(int? id)
        {
            if (LoggedIn.isLoggedIn())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Appointments appointments = db.Appointments.Find(id);
                if (appointments == null)
                {
                    return HttpNotFound();
                }
                return View(appointments);

            }
            return RedirectToAction("Login", "AccountController");


        }

        // GET: Appointments/Create
        public ActionResult Add()
        {
            if (LoggedIn.isLoggedIn())
            {
                AddAppointment addappointment = new AddAppointment();
                addappointment.Doctors = db.Doctors.ToList();
                addappointment.Patients = db.Patients.ToList();
                return View(addappointment);

            }
            return RedirectToAction("Login", "AccountController");

        }

        // POST: Appointments/Create
        [HttpPost]
        public ActionResult Add(string a_date, string a_time, int d_name, string a_reason)
        {
            if (LoggedIn.isLoggedIn())
            {
                
                DateTime currentdate = System.DateTime.Now;
                //get the current date form the system

                if (a_date != "")
                {
                    //covert the string into the date so that we can make a compare between current date and user input date
                    //this statement validates that user can not make a past date entry for the appointment
                    DateTime bookdate = DateTime.Parse(a_date);
                    
                    if (currentdate > bookdate)
                    {

                        return RedirectToAction("Index", "Error");

                    }
                    string patientId = User.Identity.GetUserId();
                    //store the id of the logged in user in the string 
                    //using this vlaue get the value form the patient 
                    //if that user filled the patient details then he/she will be able to the 
                    //book the appointment
                    //otherwise this will take them too the add patient information page 
                    //after that they can book the appointment
                    string query = "select * from patients where patientId = @id";
                    var Parameter = new SqlParameter("@id", patientId);
                    Patients patients = db.Patients.SqlQuery(query,Parameter).FirstOrDefault();

                    if (patients == null)
                    {
                        return RedirectToAction("Add", "Patients");
                    }

                    //store the counts of appointments ID so that we can add it one on every new appointment
                    //for creating the appointment reference number
                    List<Appointments> appointments = db.Appointments.ToList();
                    int count = appointments.Count();

                    Appointments newappointments = new Appointments();
                    newappointments.appointmentDate = DateTime.ParseExact(a_date + " " + a_time, "yyyy-MM-dd Hmm", CultureInfo.InvariantCulture);
                    newappointments.patientId = patientId;
                    newappointments.doctorId = d_name;
                    newappointments.appointmentReason = a_reason;
                    newappointments.appointmentReferenceNumebr = "HSN" + count + 1;
                    db.Appointments.Add(newappointments);
                    db.SaveChanges();
                    return RedirectToAction("List");


                }
            }
            return RedirectToAction("Login", "Account");



        }

        // GET: Appointments/Edit/5
        public ActionResult Edit(int id)
        {
            if (LoggedIn.isLoggedIn())
            {
                string query = "select * from appointments where appointmentId =@id";
                var Parameter = new SqlParameter("@id", id);
                Appointments appointments = db.Appointments.SqlQuery(query, Parameter).FirstOrDefault();

                List<Doctors> doctors = db.Doctors.SqlQuery("select * from doctors").ToList();
                AddAppointment viewmodel = new AddAppointment();
                viewmodel.Doctors = doctors;
                viewmodel.Appointment = appointments;
                return View(viewmodel);

            }
            return RedirectToAction("Login", "AccountController");


        }

        // POST: Appointments/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, string a_date, string a_time, int d_name, string a_reason)
        {
            if (LoggedIn.isLoggedIn())
            {
                Appointments appointments = db.Appointments.FirstOrDefault(a => a.appointmentId == id);
                appointments.appointmentDate = DateTime.ParseExact(a_date + " " + a_time, "yyyy-MM-dd Hmm", CultureInfo.InvariantCulture);
                appointments.appointmentReason = a_reason;
                appointments.doctorId = d_name;
                db.SaveChanges();
                return RedirectToAction("List");

            }
            return RedirectToAction("Login", "AccountController");



        }

        // GET: Appointments/Delete/5
        public ActionResult Delete(int id)
        {
            if (LoggedIn.isLoggedIn())
            {
                string query = "delete from appointments where appointmentId = @id";
                SqlParameter[] sqlparams = new SqlParameter[1];
                sqlparams[0] = new SqlParameter("@id", id);
                db.Database.ExecuteSqlCommand(query, sqlparams);

                return RedirectToAction("List");

            }
            return RedirectToAction("Login", "AccountController");

        }

        // POST: Appointments/Delete/5
        //[HttpPost]
        public ActionResult DeleteMultiple()
        {
            try
            {
                DateTime date = System.DateTime.Now;
                string query = "delete from appointments where appointdate < @date ";
                SqlParameter[] sqlparams = new SqlParameter[1];
                sqlparams[0] = new SqlParameter("@date", date);
                db.Database.ExecuteSqlCommand(query, sqlparams);
                return RedirectToAction("List");

            }
            catch
            {
                return View();
            }
        }
    }
}