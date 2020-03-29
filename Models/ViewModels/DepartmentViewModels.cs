using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HSNHospitalProject.Models.ViewModels
{
    public class DepartmentCreateViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string name { get; set; }
    }
}