using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalentHunt.Dtos
{
    public class ResponsibilityDto
    {
        public string Title { get; set; }
        public string Description { get; set; }

        // relation with vacancy
        public ICollection<KeyResponsibilityDto> KeyResponsibilities { get; set; }
    }
}
