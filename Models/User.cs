using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalentHunt.Models
{
    public class User : IdentityUser
    {
        public byte[] HashPassword { get; set; }
        public byte[] PasswordSalt { get; set; }

        // relation with vacancy
        // public ICollection<JobPost> JobPosts { get; set; }
    }
}
