using System.Collections.Generic;

namespace TechStore.Models.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string LastName { get; set; }

        public string Adress { get; set; }

        public string PhoneNumber { get; set; }

        public int Age { get; set; }

        public int CartId { get; set; }

        public decimal CartPrice { get; set; }

        public int CartProductCount { get; set; }
    }
}
