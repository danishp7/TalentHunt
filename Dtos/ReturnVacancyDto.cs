using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalentHunt.Dtos
{
    public class ReturnVacancyDto
    {
        public string Title { get; set; }
        public string Description { get; set; }

        // relation with responsibilities
        public ICollection<ResponsibilityDto> Responsibilities { get; set; }

        // relation with experience
        public ExperienceDto Experience { get; set; }
    }
}
