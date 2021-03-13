using System;
using System.ComponentModel.DataAnnotations;

namespace TechStore.Models.Models
{
    public class AuthenticateRequest
    {
        [Required]
        [StringLength(20, ErrorMessage = "Username can't be more than 20 characters")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Passowrd can't be more than 100 characters")]
        public string Password { get; set; }
    }
}
