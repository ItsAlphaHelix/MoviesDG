namespace MovieDG.Core.Seeding
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using MovieDG.Common;
    using MovieDG.Data.Data.Models;
    using MoviesDG.Data;
    using MoviesDG.Data.Models;
    using Newtonsoft.Json;
    using static MovieDG.Common.GlobalConstants;
    internal class Seeder : ISeeder
    {
        public async Task SeedAsync(MovieDGDbContext dbContext, IServiceProvider serviceProvider)
        {
            
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            await SeedRoleAsync(roleManager, AdminRoleName);
            await SeedRoleAsync(roleManager, SuportRoleName);
            await SeedRoleAsync(roleManager, ModeratorRoleName);

            await SeedRoleAdminToUser(userManager, roleManager);
        }

        private static async Task SeedRoleAsync(RoleManager<ApplicationRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);

            if (role == null)
            {
                var result = await roleManager.CreateAsync(new ApplicationRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }

        private static async Task SeedRoleAdminToUser(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            // Define admin user details
            string city = "Valencia";
            string country = "Spain";
            string phone = "0897543504";
            string username = "Admin";
            string adminEmail = "admin@gmail.com";
            string adminPassword = "admin23moviesdg@";

            // Check if the admin user exists
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var newAdmin = new ApplicationUser
                {
                    UserName = username,
                    Email = adminEmail,
                    Country = country,
                    City = city,
                    PhoneNumber = phone,
                    EmailConfirmed = true
                };

                var createUserResult = await userManager.CreateAsync(newAdmin, adminPassword);
                if (!createUserResult.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, createUserResult.Errors.Select(e => e.Description)));
                }

                // Ensure the Admin role exists
                var adminRole = await roleManager.FindByNameAsync(GlobalConstants.AdminRoleName);
                if (adminRole == null)
                {
                    throw new Exception($"Role {GlobalConstants.AdminRoleName} not found.");
                }

                // Assign the Admin role to the user
                var addToRoleResult = await userManager.AddToRoleAsync(newAdmin, GlobalConstants.AdminRoleName);
                if (!addToRoleResult.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, addToRoleResult.Errors.Select(e => e.Description)));
                }
            }
            else
            {
                // Ensure the user is in the Admin role
                if (!await userManager.IsInRoleAsync(adminUser, GlobalConstants.AdminRoleName))
                {
                    await userManager.AddToRoleAsync(adminUser, GlobalConstants.AdminRoleName);
                }
            }
        }
    }
}