using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TechStore.Api.Data.Enteties;

namespace TechStore.Api.Helpers
{
    public static class AuthenticatedUserData
    {
        public static int? GetCartId(HttpContext context)
        {
            try
            {
                int? cartId = Convert.ToInt32(
                context
                .User
                .Claims
                .First(c => c.Type == "cart")
                .Value);

                return cartId;
            }
            catch (Exception ex)
            {
                return null;
            }  
        }

        public static string GetUsername(HttpContext context)
        {
            try
            {
                string username = context
                .User
                .Claims
                .First(c => c.Type == ClaimTypes.Upn)
                .Value;

                return username;
            }
            catch (Exception ex)
            {
                return null;
            }            

        }

        public static string GetPassword(HttpContext context)
        {
            try
            {
                string password = context
                    .User
                    .Claims
                    .First(c => c.Type == "password")
                    .Value;

                return password;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string GetEmail(HttpContext context)
        {
            try
            {
                string email = context
                .User
                .Claims
                .First(c => c.Type == ClaimTypes.Email)
                .Value;

                return email;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static int? GetUserId(HttpContext context)
        {
            try
            {
                int? uid = Convert.ToInt32(context
                .User
                .Claims
                .First(c => c.Type == ClaimTypes.Upn)
                .Value);

                return uid;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string GetUserRole(HttpContext context)
        {
            try
            {
                string role = context
                .User
                .Claims
                .First(c => c.Type == ClaimTypes.Role)
                .Value;

                return role;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string GetUserAdress(HttpContext context)
        {
            try
            {
                string adress = context
                .User
                .Claims
                .First(c => c.Type == ClaimTypes.StreetAddress)
                .Value;

                return adress;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string GetUserPhone(HttpContext context)
        {
            try
            {
                string phone = context
                .User
                .Claims
                .First(c => c.Type == ClaimTypes.MobilePhone)
                .Value;

                return phone;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string GetUserName(HttpContext context)
        {
            try
            {
                string username = context
                .User
                .Claims
                .First(c => c.Type == ClaimTypes.Name)
                .Value;

                return username;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
