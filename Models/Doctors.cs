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

        [RegularExpression(@"^[+]*(1)*(\s)*[0-9]{3}(-|\s)*[0-9]{3}(-|\s)*[0-9]{4}*$")]
        [Required]
        [StringLength(30)]
        public string doctorPNumber { get; set; }
        public string doctorEAddress { get; set; }

        [Display(Name = "doctorJoinDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime doctorJoinDate { get; set; }
        public string doctorWorkingDays { get; set; }
        public int departmentId { get; set; }
        [ForeignKey("departmentId")]
        public virtual Department Department { get; set; }
        public ICollection<Appointments> Appointments { get; set; }
    }
}