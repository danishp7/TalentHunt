using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TalentHunt.Models;

namespace TalentHunt.Dtos
{
    public class PostVacancyDto
    {
        [Required(ErrorMessage = "Job title field must be filled.")]
        [StringLength(20, ErrorMessage = "Job title must be in between 5 and 20 characters long.", MinimumLength = 5)]
        [Display(Name = "Job Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Job Description field must be filled.")]
        [StringLength(5000, ErrorMessage = "Job description characters limit exceeding.")]
        [Display(Name = "Job Description")]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "Job salary field must be provided.")]
        [Display(Name = "Job Salary")]
        public int Salary { get; set; }

        // [Required(ErrorMessage = "User id must be provided.")]
        public ICollection<JobPostDto> JobPosts { get; set; }

        [Required(ErrorMessage = "Experience field must be filled.")]
        [Display(Name = "Job Experience")]
        public ExperienceDto Experience { get; set; }

        [Required(ErrorMessage = "Key Responsibilities must be provided.")]
        [Display(Name = "Job Responsibilities")]
        public ICollection<KeyResponsibilityDto> KeyResponsibilities { get; set; }

        [Required(ErrorMessage = "Experience field must be filled.")]
        [Display(Name = "Job Experience")]
        public EducationDto Education { get; set; }
    }
}
