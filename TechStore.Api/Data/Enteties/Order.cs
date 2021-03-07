using System.Collections.Generic;

namespace TechStore.Api.Data.Enteties
{
    public class Order
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }

        public string PhoneNumber { get; set; }

        public decimal Price { get; set; }        
    }
}
