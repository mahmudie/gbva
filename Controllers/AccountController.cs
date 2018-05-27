using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using rmc.Models.AccountViewModels;
using rmc.Data;
using System;
using rmc.Models;
using rmc.Controllers;

namespace rmc.Controllers
{
    [Authorize(Roles = "administrator")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _db;
        private readonly rmsContext _context;


        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILoggerFactory loggerFactory,
            ApplicationDbContext db,
             rmsContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _db = db;
            _context = context;
        }

        //
        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (User.IsInRole("guest"))
            {
                return RedirectToAction(nameof(HomeController.Index), "Dashboard");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]    [Authorize(Policy = "admin")]

        public async Task<IActionResult> userslist()
        {
            var users =  _db.Users.Select(m => new ApplicationUser()
            {
                UserName = m.UserName,
                FirstName = m.FirstName,
                LastName = m.LastName,
                Id = m.Id,
                Email = m.Email,
                PhoneNumber = m.PhoneNumber,
                Position = m.Position,
                Active = m.Active,
                TenantId = m.TenantId
            }).ToList();

            foreach (var item in users)
            {
                item.RoleNames = await _userManager.GetRolesAsync(item);
            }
            return View(users);
        }
    [Authorize(Policy = "admin")]

        public async Task<IActionResult> lockUser(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user.UserName != User.Identity.Name)
                {
                    if (!user.Active)
                    {
                        user.LockoutEnabled = false;
                        user.Active = true;
                    }
                    else
                    {
                        user.LockoutEnabled = true;
                        user.LockoutEnd = DateTime.Now.AddYears(50);
                        user.Active = false;
                    }
                }
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("userslist");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View();
        }
        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: true, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    _logger.LogInformation(1, "User logged in.");
                    return RedirectToLocal(returnUrl);
                }

                if (result.IsLockedOut)
                {
                    _logger.LogWarning(2, "User account locked out.");
                    return View("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Register
        [HttpGet]
        [Authorize(Policy = "admin")]

        public IActionResult Register(string returnUrl = null)
        {
            ViewBag.Role = new SelectList(_db.Roles.ToList(), "Name", "Name");
            ViewBag.Tenant = new SelectList(_context.Tenants.ToList(), "Id", "Name");
            ViewBag.Facilities = new SelectList(_context.FacilityInfo.ToList(), "FacilityId", "FacilityName");
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpGet("Account/Edit/{name}")]
        [Authorize(Policy = "admin")]

        public async Task<IActionResult> Edit(string name, string returnUrl = null)
        {
            if (name == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByNameAsync(name);
            if (user == null)
            {
                return NotFound();
            }
            var role = await _userManager.GetRolesAsync(user);

            RegisterViewModel info = new RegisterViewModel
            {
                UserName = user.UserName,
                Name = user.FirstName,
                LastName = user.LastName,
                Role = role.FirstOrDefault(),
                Position = user.Position,
                Number = user.PhoneNumber,
                Email = user.Email,
                TenantId = user.TenantId
            };
            ViewBag.Role = new SelectList(_db.Roles.ToList(), "Name", "Name");
            //ViewBag.Tenant = new SelectList(_context.Tenants.ToList(), "Id", "Name");
            ViewData["ReturnUrl"] = returnUrl;
            return View(info);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin")]

        public async Task<IActionResult> Edit(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                user.FirstName = model.Name;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.Position = model.Position;
                user.PhoneNumber = model.Number;
                user.TenantId = model.TenantId;

                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                IdentityResult passwordChangeResult = await _userManager.ResetPasswordAsync(user, resetToken, model.Password);
                var result = await _userManager.UpdateAsync(user);
                var Roles = await _userManager.GetRolesAsync(user);
                if (result.Succeeded)
                {
                    if (Roles != null)
                    {
                        foreach (var role in Roles)
                        {
                            await _userManager.RemoveFromRoleAsync(user, role);
                        }
                    }
                    await _userManager.AddToRoleAsync(user, model.Role);
                    _logger.LogInformation(3, "User created a new account with password.");
                    return RedirectToAction("userslist");
                }
                AddErrors(result);
            }

            ViewBag.Role = new SelectList(_db.Roles.ToList(), "Name", "Name");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin")]

        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    PhoneNumber = model.Number,
                    TenantId = model.TenantId,
                    FirstName = model.Name,
                    LastName = model.LastName,
                    Email = model.Email,
                    Position = model.Position,
                    Active = true
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (model.Role.Equals("administrator") && user.TenantId.Equals(1))
                    {
                        await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("super_admin", user.TenantId.ToString()));

                    }
                    await _userManager.AddToRoleAsync(user, model.Role);
                    _logger.LogInformation(3, "User created a new account with password.");
                    return RedirectToAction("userslist");
                }
                AddErrors(result);
            }

            ViewBag.Role = new SelectList(_db.Roles.ToList(), "Name", "Name");
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation(4, "User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }



        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion
    }
}
