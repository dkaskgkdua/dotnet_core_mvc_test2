using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using test2.Areas.Identity.Data;

namespace test2.Models
{
    public class SeedData
    {
        public void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                DbContextOptions<ApplicationDbContext>>()))
            using (var roleManager = serviceProvider.GetRequiredService<
                RoleManager<IdentityRole>>())
            using (var userManager = serviceProvider.GetRequiredService<
                UserManager<ApplicationUser>>())
            { 
                var adminRole = new IdentityRole { Name = "admin" };
                var userRole = new IdentityRole { Name = "user" };

                roleManager.CreateAsync(adminRole).Wait();
                roleManager.CreateAsync(userRole).Wait();

                var adminUser = new ApplicationUser
                {
                    UserName = "admin@localhost",
                    Email = "admin@localhost"
                };
                var password = "test1!A";
                var result = userManager.CreateAsync(adminUser, password).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(adminUser, adminRole.Name).Wait();
                    userManager.AddToRoleAsync(adminUser, userRole.Name).Wait();
                }
                context.SaveChanges();
            }
        }
    }
}
