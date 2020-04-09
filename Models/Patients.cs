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

        [Display(Name = "patientDOB")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string patientDOB { get; set; }
        public string patientEAddress { get; set; }

        [RegularExpression(@"^[+]*(1)*(\s)*[0-9]{3}(-|\s)*[0-9]{3}(-|\s)*[0-9]{4}*$")]
        [Required]
        [StringLength(30)]
        public string patientPNumber { get; set; }
        public string patientHAddress { get; set; }
        public string patientHealthCard { get; set; }
        public ICollection<Appointments> Appointments { get; set; }
    }
}