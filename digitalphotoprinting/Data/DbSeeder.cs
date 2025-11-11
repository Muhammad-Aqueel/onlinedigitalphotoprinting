using digitalphotoprinting.Constants;
using digitalphotoprinting.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace digitalphotoprinting.Data
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
        {
            using (var scope = service.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await dbContext.Database.MigrateAsync(); // Ensure the database is created and migrations are applied

                //Seed Roles
                var userManager = service.GetService<UserManager<ApplicationUser>>();
                var roleManager = service.GetService<RoleManager<IdentityRole>>();
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));

                // creating admin

                var user = new ApplicationUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    FName = "MyImage",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };

                var userInDb = await userManager.FindByEmailAsync(user.Email);
                if (userInDb == null)
                {
                    await userManager.CreateAsync(user, "Admin@123");
                    await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
                }
            }
        }

    }
}
