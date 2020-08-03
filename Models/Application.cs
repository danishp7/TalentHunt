using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalentHunt.Models
{
    public class Application
    {
        public int Id { get; set; }
        public string CvName { get; set; }
        public string ApplicantName { get; set; }
        public string ApplicantEmail { get; set; }
        public string ApplicantContactNumber { get; set; }
        public string ApplicantAddress { get; set; }

        // relation with educationlevel
        public int EducationLevelId { get; set; }
        public DateTime? DegreeStartDate { get; set; }
        public DateTime? DegreeEndDate { get; set; }

        // relation with employment
        public int EmploymentId { get; set; }
        public DateTime? EmploymentStartDate { get; set; }
        public DateTime? EmploymentEndDate { get; set; }

        // relation with institute
        public int InstituteId { get; set; }

        // relation with vacancy 
        public Vacancy Vacancy { get; set; }
        public int VacancyId { get; set; }

        // relaion with user
        public User AppUser { get; set; }
    }
}
