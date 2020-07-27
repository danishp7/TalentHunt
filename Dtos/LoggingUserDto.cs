using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TalentHunt.Dtos
{
    public class LoggingUserDto
    {
        [Required]
        [StringLength(20, ErrorMessage = "Job title must be in between 5 and 20 characters long.", MinimumLength = 5)]
        [Display(Name = "User Email")]
        public string Email { get; set; }
        
        [Required]
        [StringLength(50, ErrorMessage = "Job title must be in between 5 and 20 characters long.", MinimumLength = 5)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
