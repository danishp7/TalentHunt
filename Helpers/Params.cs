using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalentHunt.Helpers
{
    public class Params
    {
        public int PageNumber { get; set; } = 1;

        private int MaxPageSize = 50;
        
        int pageSize = 6;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
        public string KeyWord { get; set; } = "default";
    }
}
