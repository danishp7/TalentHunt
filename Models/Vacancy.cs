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

        // relation with user
        public ICollection<JobPost> JobPosts { get; set; }

        // relation with responsibilities
        public ICollection<KeyResponsibility> KeyResponsibilities { get; set; }

        // relation with experience
        public Experience Experience { get; set; }
        public int ExperienceId { get; set; }
    }
}
