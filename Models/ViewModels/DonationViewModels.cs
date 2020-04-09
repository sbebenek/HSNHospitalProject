using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HSNHospitalProject.Models.ViewModels
{
    public class DonationIndexViewModel {

        //Constructor
        public DonationIndexViewModel(Donation donation) {
            id = donation.donationId;
            name = donation.donationName;
            //Convert to dollar value (CAD)
            amount = (float)donation.donationAmount / 100;
            email = donation.donationEmail;
            anonymous = donation.donationAnonymous;
        }

        public int id;

        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string name { get; set; }

        [Display(Name = "Amount($)")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public float amount { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Display(Name = "Anonymous")]
        public bool anonymous { get; set; }
    }

    public class DonationCreateViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string name { get; set; }

        [Required]
        [Range(1, float.MaxValue, ErrorMessage = "The amount must be greater than 1")]
        [Display(Name = "Amount($)")]
        public float amount { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Display(Name = "Anonymous")]
        public bool anonymous { get; set; }

        //Old method of the Payment (Not safe)
        /*
        [Required]
        [StringLength(16, ErrorMessage = "The card number must be 16 digit long.", MinimumLength = 16)]
        [DataType(DataType.Text)]
        [Display(Name = "Card Number")]
        public string number { get; set; }

        [Required]
        [Range(1, 12, ErrorMessage = "The month must be within 1 to 12")]
        [Display(Name = "Card ExpMonth")]
        public int expMonth { get; set; }

        [Required]
        [Range(10, 99, ErrorMessage = "The expiration year must be a two digit number")]
        [Display(Name = "Card ExpYear")]
        public int expYear { get; set; }

        [Required]
        [StringLength(3, ErrorMessage = "The cvc number must be 3 digit long", MinimumLength = 3)]
        [DataType(DataType.Text)]
        [Display(Name = "Card CVC")]
        public string cvc { get; set; }
        */
    }
}