using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TechStore.Api.Helpers
{
    public class AdminAttribute : Attribute, IAuthorizationFilter
    {
        public AdminAttribute()
        {
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userAdminClaim = context.HttpContext.User.Claims.First(c => c.Type == "admin");

            var flaseAdminClaim = new Claim("admin", "False");

            if (userAdminClaim.Value == flaseAdminClaim.Value)
            {
                context.Result = new JsonResult(new { message = "Forbidden for normal users" }) { StatusCode = StatusCodes.Status403Forbidden };
            }
        }
    }
}
