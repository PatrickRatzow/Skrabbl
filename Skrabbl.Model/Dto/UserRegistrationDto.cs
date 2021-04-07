using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Skrabbl.Model.Dto
{
    public class UserRegistrationDto
    {
        [Required]
        [MinLength(4)]
        [MaxLength(16)]
        public string UserName { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(64)]
        public string Password { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

    }
}
