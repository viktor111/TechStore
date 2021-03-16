using System;
using System.Collections.Generic;
using TechStore.Models.Types;

namespace TechStore.Api.Data.Enteties
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Picture { get; set; }

        public CategoryType Category { get; set; }

        public ICollection<CartProduct> CartProduct { get; set; }
    }
}
