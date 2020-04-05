using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HSNHospitalProject.Models
{
    public class Patients
    {
        [Key, ForeignKey("ApplicationUser")]
        public string patientId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string patientFName { get; set; }
        public string patientLName { get; set; }
        public string patientDOB { get; set; }
        public string patientEAddress { get; set; }
        public string patientPNumber { get; set; }
        public string patientHAddress { get; set; }
        public string patientHealthCard { get; set; }
        public ICollection<Appointments> Appointments { get; set; }
    }
}