namespace MovieDG.Core.ViewModels.Users
{
    public class UserWithRolesViewModel
    {
        public IEnumerable<UserViewModel> Admins { get; set; }
        public IEnumerable<UserViewModel> Members { get; set; }
    }
}
