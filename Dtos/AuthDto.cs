﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TalentHunt.Dtos
{
    public class AuthDto
    {
        [Required]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Email must be at least 8 character long...")]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Password must be at least 5 character long...")]
        public string Password { get; set; }
    }
}
