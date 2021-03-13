using System;
using System.Security.Claims;
using TechStore.Api.Data.Enteties;

namespace TechStore.Api.Helpers
{
    public interface IClaimGenerator<T>
    {
        Claim[] GenerateClaims(User user);
    }
}
