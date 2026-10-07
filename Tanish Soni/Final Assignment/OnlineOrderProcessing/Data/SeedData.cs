using Microsoft.AspNetCore.Identity;
using OnlineOrderProcessing.Models;

namespace OnlineOrderProcessing.Data
{
    public static class SeedData
    {
        public static async Task SeedAsync(
            UserManager<ApplicationUser> userManager
       )
        {
          
            var adminEmail = "admin1212@gmail.com";

            var admin = await userManager.FindByEmailAsync(adminEmail);

            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(
                    admin,
                    "Test@1212"
                );

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(
                        admin,
                        "Admin"
                    );
                }
            }
            var customerEmail = "customer1212@gmail.com";

            var customer = await userManager.FindByEmailAsync(
                customerEmail);

            if (customer == null)
            {
                customer = new ApplicationUser
                {
                    UserName = customerEmail,
                    Email = customerEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(
                    customer,
                    "Test@1212"
                );

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(
                        customer,
                        "Customer"
                    );
                }
            }
        }
    }
}