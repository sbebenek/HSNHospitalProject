using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HSNHospitalProject.Models.ViewModels
{
    public class DoctorDepartment
    {
        //one Doctor
        public virtual Doctors Doctors { get; set; }
        //list of department
        public List<Department> departments { get; set; }
        //list of doctors 
        public List<Doctors> doctors { get; set; }
    }
}