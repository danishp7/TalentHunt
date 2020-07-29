using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TalentHunt.Dtos
{
    public class ApplicationDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Applicant's name is required")]
        [Display(Name = "Name")]
        public string ApplicantName { get; set; }

        [Required]
        //[RegularExpression(@"[A-Za-z0-9]+@[A-Za-z].com", ErrorMessage = "Your email does not match with the example given.")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Applicant's email is required")]
        [Display(Name = "Email")]
        public string ApplicantEmail { get; set; }

        [Required(ErrorMessage = "Applicant's contact number is required")]
        //[RegularExpression(@"{0\d\d\d}-{\d\d\d\d\d\d\d}", ErrorMessage = "Your contact number does not match with the example given.")]
        [StringLength(12, MinimumLength = 12)]
        [Display(Name = "Contact Number")]
        public string ApplicantContactNumber { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 8, ErrorMessage = "Applicant's address is required")]
        [Display(Name = "Address")]
        public string ApplicantAddress { get; set; }

        // relation with educationlevel
        [Required(ErrorMessage = "Education id is invalid")]
        [Display(Name = "Education Level")]
        public int EducationLevelId { get; set; }

        [Required(ErrorMessage = "Degree starting date is required")]
        [Display(Name = "Degree Starting Date")]
        public DateTime DegreeStartDate { get; set; }

        [Required(ErrorMessage = "Degree ending date is required")]
        [Display(Name = "Degree ending Date")] 
        public DateTime? DegreeEndDate { get; set; }

        // relation with employment
        [Display(Name = "Employment")]
        public int EmploymentId { get; set; }

        [Display(Name = "Degree start Date")]
        public DateTime? EmploymentStartDate { get; set; }

        [Display(Name = "Degree ending Date")]
        public DateTime? EmploymentEndDate { get; set; }

        // relation with institute
        [Required(ErrorMessage = "Institute id is invalid")]
        public int InstituteId { get; set; }

        // relation with vacancy
        [Required(ErrorMessage = "Vacancy Id is invalid!")]
        public int VacancyId { get; set; }
    }
}
