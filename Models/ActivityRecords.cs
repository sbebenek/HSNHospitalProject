using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HSNHospitalProject.Models
{
    public class ActivityRecords
    {
        [Key]
        public int activityrecordid { get; set; }

        //the weekday the rating was made (Idk if this is the best way but 
        //use the DateTime object in the controller to get the weekday as a string and store that)
        public string activityrecorddate { get; set; }

        //the rating of how busy a given day was, from 1-10
        public int activityrecordrating { get; set; }
    }
}