using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HSNHospitalProject.Models
{
    public class Department
    {
        [Key]
        public int departmentId { get; set; }

        //A Department has a name
        public string departmentName { get; set; }

        //A Department can have many JobPost (One to Many Relationship)
        public ICollection<JobPost> JobPosts { get; set; }
    }
}