using System;
using System.Collections.Generic;

namespace TechStore.Models.Models
{
    public class CartModel
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public int ProductCount { get; set; }

        public IEnumerable<CartProductModel> CartProduct { get; set; }

        public UserModel User { get; set; }
    }
}
