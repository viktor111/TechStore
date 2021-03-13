using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace TechStore.Api.Helpers
{
    public static class Authenticator
    {

        public static SigningCredentials GenerateSignature()
        {
            var secretBytes = Encoding.UTF8.GetBytes(Constants.Secret);
            var key = new SymmetricSecurityKey(secretBytes);

            var algorithm = SecurityAlgorithms.HmacSha256;

            var signatureCredentials = new SigningCredentials(key, algorithm);

            return signatureCredentials;
        }

        public static string GenerateToken(Claim[] claims, int daysUntilExpire)
        {
            var token = new JwtSecurityToken(
                Constants.Issuer,
                Constants.Audiance,
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(daysUntilExpire),
                GenerateSignature()
                );

            var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenJson;
        }
    }
}
