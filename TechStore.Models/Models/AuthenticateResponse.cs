using System;
namespace TechStore.Models.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Token { get; set; }

        public AuthenticateResponse(UserModel user, string token)
        {
            Id = user.Id;
            FirstName = user.Name;
            LastName = user.LastName;
            Username = user.Username;
            Token = token;
        }
    }
}
