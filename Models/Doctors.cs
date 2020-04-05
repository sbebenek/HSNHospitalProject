using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HSNHospitalProject.Models
{
    public class Doctors
    {
        [Key]
        public int doctorId { get; set; }
        public string doctorFName { get; set; }
        public string doctorLName { get; set; }
        public DateTime doctorDOB { get; set; }
        public string doctorPNumber { get; set; }
        public string doctorEAddress { get; set; }
        public DateTime doctorJoinDate { get; set; }
        public int departmentId { get; set; }
        [ForeignKey("departmentId")]
        public virtual Department Department { get; set; }
        public ICollection<Appointments> Appointments { get; set; }
    }
}