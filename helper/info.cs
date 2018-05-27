using System.Security.Claims;
using System.Threading.Tasks;
using rmc.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;

public class MyUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
{
    public MyUserClaimsPrincipalFactory (
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<IdentityOptions> optionsAccessor) : base(userManager, roleManager, optionsAccessor)
    {
    }

    public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
    {
        var principal = await base.CreateAsync(user);

        //Putting our Property to Claims
        //I'm using ClaimType.Email, but you may use any other or your own
        ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
        new Claim(ClaimTypes.Email, user.TenantId.ToString())
    });

        return principal;
    }
}