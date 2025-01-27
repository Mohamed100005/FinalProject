using DbInitailizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Models.ViewModels;
using Models;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Airline.Areas.Identity.Controllers
{
    public class AccountController : Controller
    {
        private readonly IDbInitialize dbInitialize;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(IDbInitialize dbInitialize, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            this.dbInitialize = dbInitialize;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }
        public IActionResult Register()
        {
            dbInitialize.Init();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM, IFormFile file)
        {
            ModelState.Remove("file");
            string? FileName = null;

            var EmailExist = await userManager.FindByEmailAsync(registerVM.Email);
            var UserExist = await userManager.FindByNameAsync(registerVM.UserName);
            if (EmailExist != null)
            {
                ModelState.AddModelError("Email", "this email is already exists");
            }
            if (UserExist != null)
            {
                ModelState.AddModelError("UserName", "this user name is already exists");
            }
            if (registerVM.Date >= DateOnly.FromDateTime(DateTime.Now.AddYears(-10)))
            {
                ModelState.AddModelError("Date", "users under 10 years can not use this site");
            }

            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    FileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", FileName);
                    using (var stream = System.IO.File.Create(FilePath))
                    {
                        file.CopyTo(stream);
                    }
                }
                DateOnly DateNow = DateOnly.FromDateTime(DateTime.Now);
                int age = DateNow.Year - registerVM.Date.Year;
                ApplicationUser user = new()
                {
                    Name = registerVM.Name,
                    UserName = registerVM.UserName,
                    Email = registerVM.Email,
                    Age = age,
                    ImageUrl = FileName
                };
                var Result = await userManager.CreateAsync(user, registerVM.Password);
                if (Result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, NameInit.Customer);
                    return RedirectToAction(nameof(Login));
                }
            }
            return View(registerVM);
        }
        public async Task<IActionResult> Login()
        {
            var externallogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            LoginVM loginVM = new()
            {
                ExternalLogins = externallogins
            };
            return View(loginVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            ModelState.Remove("ExternalLogins");
            if (ModelState.IsValid)
            {
                var EmailExist = await userManager.FindByEmailAsync(loginVM.Account);
                var UserExist = await userManager.FindByNameAsync(loginVM.Account);
                if (EmailExist != null || UserExist != null)
                {
                    var result = await userManager.CheckPasswordAsync(EmailExist ?? UserExist, loginVM.Password);
                    if (result)
                    {
                        await signInManager.SignInAsync(EmailExist ?? UserExist, loginVM.Check);
                        return RedirectToAction("Index", "Home", new { area = "Customer" });
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid username or password");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password");
                }
            }
            return View(loginVM);
        }
        public IActionResult ExternalLogin(string provider)
        {
            var RedirectUrl = Url.Action(nameof(ExternalLoginCallBack));
            var Properties = signInManager.ConfigureExternalAuthenticationProperties(provider, RedirectUrl);
            return Challenge(Properties, provider);
        }
        public async Task<IActionResult> ExternalLoginCallBack()
        {
            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }
            var ExternalLog = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            if (ExternalLog.Succeeded)
            {
                return RedirectToAction("Index", "Home", new { area = "Customer" });
            }
            if (ExternalLog.IsLockedOut)
            {
                return RedirectToAction(nameof(Login));
            }
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            if (email != null)
            {
                ApplicationUser user = new()
                {
                    Email = email,
                    UserName = email
                };
                var userCreate = await userManager.CreateAsync(user);
                if (userCreate.Succeeded)
                {
                    var result = await userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        await userManager.AddToRoleAsync(user, NameInit.Customer);
                        return RedirectToAction("Index", "Home", new { area = "Customer" });
                    }
                }
            }
            return RedirectToAction(nameof(Login));
        }
        public IActionResult Forgot()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Forgot(CheckMailVM checkMail)
        {
            var EmailExist = await userManager.FindByEmailAsync(checkMail.Email);
            if (EmailExist == null)
            {
                ModelState.AddModelError("Email", "email is wrong");
            }
            if (ModelState.IsValid)
            {
                if (EmailExist != null)
                {
                    CookieOptions cookieOptions = new CookieOptions();
                    cookieOptions.Expires = DateTime.Now.AddYears(1000);
                    Response.Cookies.Append("email", checkMail.Email, cookieOptions);
                    return RedirectToAction(nameof(Reset));
                }
            }
            return View(checkMail);
        }
        public IActionResult Reset()
        {
            var email = Request.Cookies["email"];
            ResetVM resetVM = new()
            {
                Email = email
            };
            return View(resetVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reset(ResetVM resetVM)
        {
            var EmailExist = await userManager.FindByEmailAsync(resetVM.Email);
            if (EmailExist == null)
            {
                return RedirectToAction(nameof(Login));
            }
            if (ModelState.IsValid)
            {
                ApplicationUser user = new()
                {
                    UserName=EmailExist.UserName,
                    Name = EmailExist.Name,
                    Email = EmailExist.Email,
                    Age = EmailExist.Age,
                    ImageUrl = EmailExist.ImageUrl,
                    Details = EmailExist.Details,
                };
                var roles = await userManager.GetRolesAsync(EmailExist);
                string RoleName = roles[0];
                await userManager.RemoveFromRolesAsync(EmailExist, roles);
                await userManager.DeleteAsync(EmailExist);
                var result = await userManager.CreateAsync(user, resetVM.Password);
                if (result.Succeeded)
                {
                    CookieOptions cookieOptions = new CookieOptions();
                    cookieOptions.Expires = DateTime.Now.AddYears(-1);
                    Response.Cookies.Append("email", resetVM.Email, cookieOptions);
                    await userManager.AddToRoleAsync(user, RoleName);
                    return RedirectToAction(nameof(Login));
                }
            }
            return View(resetVM);
        }
    }
}
