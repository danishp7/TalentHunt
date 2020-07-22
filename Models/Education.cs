using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalentHunt.Models
{
    public class Education
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        // relation with vacancies
        public ICollection<Vacancy> Vacancies { get; set; }
    }
}
