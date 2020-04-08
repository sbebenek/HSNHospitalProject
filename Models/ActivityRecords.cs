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


        /// <summary>
        /// The weekday the rating was made, in the DateTime format.
        /// </summary>
        [Required(ErrorMessage = "Please enter a date.")]
        public DateTime activityrecorddate { get; set; }

        /// <summary>
        /// The rating of how busy a day was, on a scale from 1-10.
        /// The exact value set is up to the jurisdication of the person making the record
        /// </summary>
        [Required(ErrorMessage = "Please enter a number from 1-10.")]
        [Range(1,10, ErrorMessage = "Please enter a number from 1-10.")]
        public int activityrecordrating { get; set; }
    }
}