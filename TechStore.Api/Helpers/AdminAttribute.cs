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

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var userAdminClaim = context.HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Role);

                var flaseAdminClaim = new Claim(ClaimTypes.Role, "False");

                if (userAdminClaim.Value == flaseAdminClaim.Value)
                {
                    context.Result = new JsonResult(new { message = "Forbidden for normal users" }) { StatusCode = StatusCodes.Status403Forbidden };
                }
            }
            catch (Exception ex)
            {
                context.Result = new JsonResult(new { message = "Forbidden for normal users" }) { StatusCode = StatusCodes.Status403Forbidden };
            }

        }
    }
}
