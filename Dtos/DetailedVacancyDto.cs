﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalentHunt.Dtos
{
    public class DetailedVacancyDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Salary { get; set; }

        // relation with responsibilities
        public ICollection<KeyResponsibilityDto> KeyResponsibilities { get; set; }

        // relation with experience
        public ExperienceDto Experience { get; set; }

        // relation with education 
        public EducationDto Education { get; set; }
    }
}
