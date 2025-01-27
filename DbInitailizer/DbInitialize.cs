using Microsoft.AspNetCore.Identity;
using Models;

namespace DbInitailizer
{
    public class DbInitialize : IDbInitialize
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public DbInitialize(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public void Init()
        {
            if (!roleManager.Roles.Any())
            {
                roleManager.CreateAsync(new(NameInit.Admin)).GetAwaiter().GetResult();
                roleManager.CreateAsync(new(NameInit.Customer)).GetAwaiter().GetResult();
                roleManager.CreateAsync(new(NameInit.Company)).GetAwaiter().GetResult();

                ApplicationUser user = new()
                {
                    Name = "Admin",
                    UserName = "Admin",
                    Email = "Admin@gmail.com",
                    Age = 30,
                    ImageUrl = "admin.png"
                };
                userManager.CreateAsync(user, "A12345t#").GetAwaiter().GetResult();

                userManager.AddToRoleAsync(user, NameInit.Admin).GetAwaiter().GetResult();
            }
        }
    }
}
