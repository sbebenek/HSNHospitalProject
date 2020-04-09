using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HSNHospitalProject.Models
{
    public class Appointments
    {
        [Key]
        public int appointmentId { get; set; }

        [Display(Name = "appointmentDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime appointmentDate { get; set; }
        public string appointmentReferenceNumebr { get; set; }
        public string appointmentReason { get; set; }
        //Representing the One doctor (One doctor to many appointment)
        public int doctorId { get; set; }
        [ForeignKey("doctorId")]
        public virtual Doctors Doctor { get; set; }
        //Representing the One patient (One patient to many appointment)
        public string patientId { get; set; }
        [ForeignKey("patientId")]
        public virtual Patients Patient { get; set; }
    }
}