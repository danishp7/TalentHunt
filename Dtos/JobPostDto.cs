using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalentHunt.Models;

namespace TalentHunt.Dtos
{
    public class JobPostDto
    {
        public int UserId { get; set; }
        public int VacancyId { get; set; }
    }
}
