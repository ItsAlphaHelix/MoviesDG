namespace MovieDG.Core.Contracts
{
    using MovieDG.Core.ViewModels.Users;
    public interface IRoleService
    {
        Task AddUserToRoleAsync(string userName, string role);

        Task<UserWithRolesViewModel> GetAllUsersRolesAsync();

        Task RemoveRoleFromUser(string userName, string role);
    }
}
