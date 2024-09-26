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
        public RoleService(
            UserManager<ApplicationUser> userManager,
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
            var moderatorRole = await roleManager.FindByNameAsync(GlobalConstants.ModeratorRoleName);
            var adminRole = await roleManager.FindByNameAsync(GlobalConstants.AdminRoleName);
            var supportRole = await roleManager.FindByNameAsync(GlobalConstants.SuportRoleName);

            if (moderatorRole == null)
            {
                throw new ArgumentException(ErrorMessageConstants.MemberRoleCanNotBeNull);
            }
            else if (adminRole == null)
            {
                throw new ArgumentException(ErrorMessageConstants.AdminRoleCanNotBeNull);
            }
            else if (supportRole == null)
            {
                throw new ArgumentException(ErrorMessageConstants.SuportRoleCanNotBeNull);
            }

            var adminUsers = await userManager.GetUsersInRoleAsync(adminRole.Name);

            var admins = adminUsers
                .Where(x => x.UserName != GlobalConstants.AdminRoleName)
                .Select(x => new UserViewModel()
            {
                Role = GlobalConstants.AdminRoleName,
                UserName = x.UserName,
            });

            var moderatorUsers = await userManager.GetUsersInRoleAsync(moderatorRole.Name);

            var moderators = moderatorUsers.Select(x => new UserViewModel()
            {
                Role = GlobalConstants.ModeratorRoleName,
                UserName = x.UserName,
            });

            var suportUsers = await userManager.GetUsersInRoleAsync(supportRole.Name);

            var suports = suportUsers.Select(x => new UserViewModel()
            {
                Role = GlobalConstants.SuportRoleName,
                UserName = x.UserName
            });

            var usersWithRoles = new UserWithRolesViewModel()
            {
                Admins = admins,
                Moderators = moderators,
                Suports = suports
            };

            return usersWithRoles;
        }


        public async Task RemoveRoleFromUser(string userName, string role, string loginUsername)
        {
            var user = await userManager.FindByNameAsync(userName);

            if (user == null)
            {
                throw new ArgumentException(ErrorMessageConstants.UserNotFound);
            }

            bool isRoleExist = await roleManager.RoleExistsAsync(role);
            bool isUserAdmin = await userManager.IsInRoleAsync(user, GlobalConstants.AdminRoleName);

            if (isRoleExist == false)
            {
                throw new ArgumentException(ErrorMessageConstants.RoleDoesNotExist);
            }
            else if (isUserAdmin &&  loginUsername != GlobalConstants.AdminRoleName)
            {
                throw new ArgumentException("You cant delete role admin");
            }
            await userManager.RemoveFromRoleAsync(user, role);
        }
    }
}
