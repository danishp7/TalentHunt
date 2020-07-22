using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalentHunt.Models
{
    public class Application
    {
        public int Id { get; set; }
        public string ApplicantName { get; set; }
        public string ApplicantEmail { get; set; }
        public int ApplicantContactNumber { get; set; }
        public string ApplicationAddress { get; set; }

        // relation with educationlevel
        public EducationLevel EducationLevel { get; set; }
        public int EducationLevelId { get; set; }
        public DateTime? DegreeStartDate { get; set; }
        public DateTime? DegreeEndDate { get; set; }

        // relation with employment
        public Employment Employment { get; set; }
        public int EmploymentId { get; set; }
        public DateTime? EmployerStartDate { get; set; }
        public DateTime? EmploymentEndDate { get; set; }

        // relation with institute
        public Institute Institute { get; set; }
        public int InstituteId { get; set; }
    }
}
