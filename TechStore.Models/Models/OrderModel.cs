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

        public List<ProductModel> Products { get; set; }        
    }
}
