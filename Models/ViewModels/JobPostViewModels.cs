using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HSNHospitalProject.Models.ViewModels
{
    public enum JobType
    {
        Job,
        Volunteer
    }

    public class JobPostCreateViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string name { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Type")]
        public JobType type { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Department")]
        public int departmentId { get; set; }
        public List<SelectListItem> allDepartments { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The experience must be equal or greater than 0")]
        [Display(Name = "Experience")]
        public int experience { get; set; }

        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "The salary must be equal or greater than 0")]
        [Display(Name = "Salary")]
        public float salary { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Content")]
        public string content { get; set; }

    }
}