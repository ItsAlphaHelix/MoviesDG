namespace MovieDG.Core.Seeding
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using MovieDG.Common;
    using MovieDG.Data.Data.Models;
    using MoviesDG.Data;
    using static MovieDG.Common.GlobalConstants;
    internal class RolesSeeder : ISeeder
    {
        public async Task SeedAsync(MovieDGDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            await SeedRoleAsync(roleManager, AdminRoleName);
            await SeedRoleAsync(roleManager, MemberRoleName);
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
    }
}