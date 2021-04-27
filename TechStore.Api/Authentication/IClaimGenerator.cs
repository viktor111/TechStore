using System.Security.Claims;
using TechStore.Api.Data.Enteties;

namespace TechStore.Api.Authentication
{
    public interface IClaimGenerator<T>
    {
        Claim[] GenerateClaims(User user);
    }
}