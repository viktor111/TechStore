using System;
using TechStore.Api.Data.Enteties;

namespace TechStore.Api.Helpers
{
    public class TokenFactory
    {
        private readonly User _user;
        private readonly int _daysUntilTokenExpires;

        public TokenFactory(User user, int daysUntilTokenExpires)
        {
            _user = user;
            _daysUntilTokenExpires = daysUntilTokenExpires;
        }

        public string CreateToken()
        {
            if(_user.IsAdmin is true)
            {
                var adminClaimsGenerator = new AdminClaims();

                var adminClaims = adminClaimsGenerator.GenerateClaims(_user);

                var adminToken = Authenticator.GenerateToken(adminClaims, _daysUntilTokenExpires);

                return adminToken;
            }
            if(_user.IsAdmin is false)
            {
                var userClaimsGenerator = new AdminClaims();

                var userClaims = userClaimsGenerator.GenerateClaims(_user);

                var userToken = Authenticator.GenerateToken(userClaims, _daysUntilTokenExpires);

                return userToken;
            }

            return null;
        }
    }
}
