using System;
using System.ComponentModel.DataAnnotations;

namespace TechStore.Models.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name can't be more than 50 characters")]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [StringLength(234, ErrorMessage = "Description can't be more than 50 characters")]
        public string Description { get; set; }
    }
}
