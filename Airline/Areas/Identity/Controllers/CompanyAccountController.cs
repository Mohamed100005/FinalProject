using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ViewModels;

namespace Airline.Areas.Identity.Controllers
{
    public class CompanyAccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public CompanyAccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CompRegVM compRegVM, IFormFile file)
        {
            ModelState.Remove("file");
            var EmailExist = await userManager.FindByEmailAsync(compRegVM.Email);
            var UserExist = await userManager.FindByNameAsync(compRegVM.UserName);
            if (EmailExist != null)
            {
                ModelState.AddModelError("Email", "this email is already exists");
            }
            if (UserExist != null)
            {
                ModelState.AddModelError("UserName", "this username is already exists");
            }
            if (compRegVM.Date > DateOnly.FromDateTime(DateTime.Now))
            {
                ModelState.AddModelError("Date", "this time is not yet arrived");
            }
            if (ModelState.IsValid)
            {
                string? fileName = null;
                if (file != null && file.Length > 0)
                {
                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(stream);
                    }
                }
                int age = DateOnly.FromDateTime(DateTime.Now).Year - compRegVM.Date.Year;
                ApplicationUser user = new()
                {
                    Name = compRegVM.Name,
                    UserName = compRegVM.UserName,
                    Email = compRegVM.Email,
                    Details = compRegVM.Detail,
                    ImageUrl = fileName,
                    Age = age,
                    Address= compRegVM.Address,
                };
                var result = await userManager.CreateAsync(user, compRegVM.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, NameInit.Company);
                    return RedirectToAction("Login", "Account");
                }
            }
            return View(compRegVM);
        }
    }
}
