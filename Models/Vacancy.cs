using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalentHunt.Models
{
    public class Vacancy
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Salary { get; set; }
        public User AppUser { get; set; }

        // relation with responsibilities
        public ICollection<KeyResponsibility> KeyResponsibilities { get; set; }

        // relation with experience
        public Experience Experience { get; set; }
        public int ExperienceId { get; set; }

        // relation with education
        public Education Education { get; set; }
        public int EducationId { get; set; }

        // relation with application
        public ICollection<Application> Applications { get; set; }
    }
}
