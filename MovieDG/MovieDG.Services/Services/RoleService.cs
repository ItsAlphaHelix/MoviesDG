namespace MovieDG.Core.Services
{
    using Microsoft.AspNetCore.Identity;
    using MovieDG.Common;
    using MovieDG.Core.Contracts;
    using MovieDG.Core.ErrorMessages;
    using MovieDG.Core.ViewModels.Users;
    using MovieDG.Data.Data.Models;

    public class RoleService : IRoleService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        public RoleService(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task AddUserToRoleAsync(string userName, string role)
        {
            var user = await userManager.FindByNameAsync(userName);

            if (user == null)
            {
                throw new ArgumentException(ErrorMessageConstants.UserNotFound);
            }

            var isRoleExist = await roleManager.RoleExistsAsync(role);

            if (isRoleExist == false)
            {
                throw new ArgumentException(ErrorMessageConstants.RoleDoesNotExist);
            }

            var isUserInRole = await userManager.IsInRoleAsync(user, role);
            
            if (isUserInRole == true)
            {
                throw new ArgumentException(String.Format(ErrorMessageConstants.AlreadyAssignedRoleToUser, role));
            }

            await userManager.AddToRoleAsync(user, role);
        }

        public async Task<UserWithRolesViewModel> GetAllUsersRolesAsync()
        {
            var memberRole = await roleManager.FindByNameAsync(GlobalConstants.MemberRoleName);
            var adminRole = await roleManager.FindByNameAsync(GlobalConstants.AdminRoleName);

            if (memberRole == null)
            {
                throw new ArgumentException(ErrorMessageConstants.MemberRoleCanNotBeNull);
            }

            if (adminRole == null)
            {
                throw new ArgumentException(ErrorMessageConstants.AdminRoleCanNotBeNull);
            }

            var adminUsers = await userManager.GetUsersInRoleAsync(adminRole.Name);

            var admins = adminUsers
                .Where(x => x.Email != GlobalConstants.AppEmail)
                .Select(x => new UserViewModel()
            {
                UserName = x.UserName,
            });

            var memberUsers = await userManager.GetUsersInRoleAsync(memberRole.Name);

            var members = memberUsers.Select(x => new UserViewModel()
            {
                UserName = x.UserName,
            });

            var usersWithRoles = new UserWithRolesViewModel()
            {
                Admins = admins,
                Members = members
            };

            return usersWithRoles;
        }


        public async Task RemoveRoleFromUser(string userName, string role)
        {
            var user = await userManager.FindByNameAsync(userName);

            if (user == null)
            {
                throw new ArgumentException(ErrorMessageConstants.UserNotFound);
            }

            var isRoleExist = await roleManager.RoleExistsAsync(role);

            if (isRoleExist == false)
            {
                throw new ArgumentException(ErrorMessageConstants.RoleDoesNotExist);
            }


            await userManager.RemoveFromRoleAsync(user, role);
        }
    }
}
