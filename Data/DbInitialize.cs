using rmc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Linq;

namespace rmc.Data
{
    public static class DbInitializer
    {
        public async static void Initialize(rmsContext db, RoleManager<IdentityRole> _roleManager, UserManager<ApplicationUser> _userManager)
        {
            db.Database.EnsureCreated();

            // Look for any students.
            if (_roleManager.Roles.Any()||_userManager.Users.Any())
            {
                return;   // DB has been seeded
            }

            IdentityRole Role = new IdentityRole();
            Role.NormalizedName = "ADMINISTRATOR";
            Role.Name ="administrator";
             await _roleManager.CreateAsync(Role);
            IdentityRole Role2 = new IdentityRole();
            Role2.NormalizedName = "DATAENTRY";
            Role2.Name = "dataentry";
            await _roleManager.CreateAsync(Role2);
            IdentityRole Role3 = new IdentityRole();
            Role3.NormalizedName = "GUEST";
            Role3.Name = "guest";
            await _roleManager.CreateAsync(Role3);
            var user = new ApplicationUser
            {
                UserName = "admins",
                PhoneNumber ="99999999",
                FirstName = "admin",
                LastName = "admin",
                Email = "admin@admin.com",
                Position = "admin",
                Active = true,
                TenantId=1
            };
            var result = await _userManager.CreateAsync(user, "159*951-Aa");
            if (result.Succeeded)
            {
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("super_admin", "1"));                
                await _userManager.AddToRoleAsync(user,"administrator");
            }
        }
    }
}