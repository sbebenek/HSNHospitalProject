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
    public class AppointmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Appointments
        public ActionResult List()
        {
            string id = User.Identity.GetUserId();
            Debug.WriteLine(id);

            List<Appointments> appointments;
            appointments = db.Appointments.Where(a => a.patientId.Contains(id)).ToList();


            return View(appointments);

        }

        // GET: Appointments/Details/5
        public ActionResult Show(int? id)
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

        // GET: Appointments/Create
        public ActionResult Add()
        {
            AddAppointment addappointment = new AddAppointment();
            addappointment.Doctors = db.Doctors.ToList();
            addappointment.Patients = db.Patients.ToList();
            return View(addappointment);
        }

        // POST: Appointments/Create
        [HttpPost]
        public ActionResult Add(string a_date, string a_time, int d_name, string a_reason)
        {
            string patientId = User.Identity.GetUserId();

            if (a_date != "")
            {
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


            }
            //return Redirect("https://localhost:44315/Success/List");
            return RedirectToAction("List");

        }

        // GET: Appointments/Edit/5
        public ActionResult Edit(int id)
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

        // POST: Appointments/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, string a_date, string a_time, int d_name, string a_reason)
        {
            Appointments appointments = db.Appointments.FirstOrDefault(a => a.appointmentId == id);
            appointments.appointmentDate = DateTime.ParseExact(a_date + " " + a_time, "yyyy-MM-dd Hmm", CultureInfo.InvariantCulture);
            appointments.appointmentReason = a_reason;
            appointments.doctorId = d_name;
            db.SaveChanges();
            return RedirectToAction("List");


        }

        // GET: Appointments/Delete/5
        public ActionResult Delete(int id)
        {
            string query = "delete from appointments where appointmentId = @id";
            SqlParameter[] sqlparams = new SqlParameter[1];
            sqlparams[0] = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("List");

        }

        // POST: Appointments/Delete/5
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
