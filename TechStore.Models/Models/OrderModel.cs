using System;
using System.Collections.Generic;

namespace TechStore.Models.Models
{
    public class OrderModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }

        public string PhoneNumber { get; set; }

        public decimal Price { get; set; }

        public int UserId { get; set; }

        public int ProductCount { get; set; }

        public ICollection<ProductModel> Products { get; set; }        
    }
}
