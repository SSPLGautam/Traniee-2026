using Microsoft.AspNetCore.Identity;
using TaskManagementSystemApi.Models;

namespace TaskManagementSystemApi.Data
{
    public class DbSeeder
    {
        public static async Task SeedRolesAndUsersAsync(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager)
        {
            string[] roles =
            {
                "Admin",
                "Manager",
                "Employee"
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(
                        new IdentityRole(role));
                }
            }

            var adminEmail = "admin@admin.com";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Admin",
                    LastName = "Authorize",
                    EmailConfirmed = true
                };

                var result =
                    await userManager.CreateAsync(
                        adminUser,
                        "Admin@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(
                        adminUser,
                        "Admin");
                }
            }

            var managerEmail = "manager@manager.com";

            var managerUser = await userManager.FindByEmailAsync(managerEmail);

            if (managerUser == null)
            {
                managerUser = new ApplicationUser
                {
                    UserName = managerEmail,
                    Email = managerEmail,
                    FirstName = "Default",
                    LastName = "Manager",
                    EmailConfirmed = true
                };

                var result =
                    await userManager.CreateAsync(
                        managerUser,
                        "Manager@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(
                        managerUser,
                        "Manager");
                }
            }
        }
    }
}

