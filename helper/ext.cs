using System.Linq;
using System.Security.Claims;
using rmc.Models;
using Microsoft.AspNetCore.Identity;

namespace rmc.Extensions
{
    public static class MyUserPrincipalExtension
    {
        public static string MyProperty(this ClaimsPrincipal user)
        {
            if (user.Identity.IsAuthenticated)
            {
                return user.Claims.FirstOrDefault(v => v.Type == ClaimTypes.Email)?.Value;
            }

            return "";
        }
    }
}