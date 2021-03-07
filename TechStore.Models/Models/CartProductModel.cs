using System;
namespace TechStore.Models.Models
{
    public class CartProductModel
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }

        public CartModel Cart { get; set; }
        public ProductModel Product { get; set; }

        public int Quantity { get; set; }


    }
}
