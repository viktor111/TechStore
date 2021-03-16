using System;
using System.Security.Claims;
using TechStore.Api.Data.Enteties;

namespace TechStore.Api.Helpers
{
    public abstract class ClaimGenerator<T> : IClaimGenerator<T>
        where T : class
    {
        public virtual Claim[] GenerateClaims(User user)
        {
            var claims = new[] {

                    new Claim(ClaimTypes.Upn, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("password", user.Password),
                    new Claim(ClaimTypes.Role, user.IsAdmin.ToString()),
                    new Claim("cart", user.CartId.ToString()),
                    new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                    new Claim(ClaimTypes.StreetAddress, user.Adress),
                    new Claim(ClaimTypes.Name, user.Name)
            };

            return claims;
        }        
    }
}
