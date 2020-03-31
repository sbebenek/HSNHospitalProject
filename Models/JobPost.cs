using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HSNHospitalProject.Models
{
    public class JobPost
    {
        [Key]
        public int jobPostId { get; set; }

        //Posting can only be a Job or Volunteer
        public string jobPostType { get; set; }

        //JobPost has a name
        public string jobPostName { get; set; }

        //A JobPost can only have one Department (One to Many Relationship)
        public int departmentId { get; set; }
        [ForeignKey("departmentId")]
        public virtual Department department { get; set; }


        //JobPost has a experience (Ex. 1 year experience requirement)
        public int jobPostExperience { get; set; }

        //JobPost has a filled flag (if the job is taken or not)
        public bool jobPostFilled { get; set; }

        //JobPost has a salary rate
        public float jobPostSalary { get; set; }

        //JobPost has a posted date
        public DateTime jobPostPostedDate { get; set; }

        //JobPost has a closed date
        public Nullable<DateTime> jobPostClosedDate { get; set; }

        //JobPost has a content (description of the job itself)
        public string jobPostContent { get; set; }
    }
}