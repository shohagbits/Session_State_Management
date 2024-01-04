using Login_Auth.Controllers;
using Login_Auth.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Payra.DataManager.Models;

namespace Payra.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDistributedCache _iMemoryCache;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger, IConfiguration configuration, RoleManager<ApplicationRole> ApplicationRole, IDistributedCache iMemoryCache, IHttpContextAccessor httpContextAccessor)
        {
            _roleManager = ApplicationRole;
            if (logger != null) { _logger = logger; }
            _userManager = userManager;
            _signInManager = signInManager;

            _configuration = configuration;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _iMemoryCache = iMemoryCache;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser user = await _userManager.FindByNameAsync(model.UserName);

                    if (user != null && !user.IsDeleted)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, model.Key, model.RememberMe, lockoutOnFailure: true);
                        if (result.Succeeded)
                        {
                            _httpContextAccessor.HttpContext.Response.Cookies.Append("cookieBranchId", model.UserSubTypeId.ToString(), new CookieOptions() { Secure = true, HttpOnly = true, SameSite = SameSiteMode.None, Expires = DateTime.Now.AddDays(20) });
                            return RedirectToLocal(returnUrl);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }     
            return View(model);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(AccountController.Login), "Account");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
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
