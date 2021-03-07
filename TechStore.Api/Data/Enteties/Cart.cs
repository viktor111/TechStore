using System;
using System.Collections.Generic;

namespace TechStore.Api.Data.Enteties
{
    public class Cart
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public int ProductCount { get; set; }

        public ICollection<CartProduct> CartProduct { get; set; }

        public User User { get; set; }
    }
}
