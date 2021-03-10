using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechStore.Models.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Name can't be more than 20 characters")]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage ="Invalid email adress")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Password can't be more than 100 characters")]
        public string Password { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Last Name can't be more than 50 characters")]
        public string LastName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Adress can't be more than 100 characters")]
        public string Adress { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [Range(1,120, ErrorMessage ="You can't be less than 1 or older than 120")]
        public int Age { get; set; }

        public int CartId { get; set; }

        public decimal CartPrice { get; set; }

        public int CartProductCount { get; set; }
    }
}
