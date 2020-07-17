using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalentHunt.Models
{
    public class Experience
    {
        public int Id { get; set; }
        public int Years { get; set; }
        public string Description { get; set; }

        // relation with vacance
        public ICollection<Vacancy> Vacancies { get; set; }
    }
}
