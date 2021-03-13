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
                    new Claim("admin", user.IsAdmin.ToString()),
                    new Claim("cart", user.CartId.ToString())
            };

            return claims;
        }        
    }
}
