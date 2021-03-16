using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace TechStore.Api.Data.Enteties
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string LastName { get; set; }

        public string Adress { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsAdmin { get; set; }

        public int Age { get; set; }

        public Cart Cart { get; set; }

        public int CartId { get; set; }

        ICollection<Order> Orders { get; set; }
    }
}
