using System;
namespace TechStore.Api.Data.Enteties
{
    public class CartProduct
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }

        public Cart Cart { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
