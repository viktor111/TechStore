using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TechStore.Api.Authentication
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
                Constants.Audience,
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