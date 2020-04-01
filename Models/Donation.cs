using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HSNHospitalProject.Models
{
    public class Donation
    {
        [Key]
        public int donationId { get; set; }

        //A Donation has a name
        public string donationName { get; set; }

        //A Donation has a amount ($CAD and value is whole number by multiplying by 100)
        //Ex. $25.50 -> 2550 stored in the model 
        public int donationAmount { get; set; }

        //A Donation has a email
        public string donationEmail { get; set; }

        //A Donation has a anonymous flag
        public bool donationAnonymous { get; set; }

        //This is the id for the payment in the Stripe API (Unique identifier for the object)
        public string donationReceiptId { get; set; }
    }
}