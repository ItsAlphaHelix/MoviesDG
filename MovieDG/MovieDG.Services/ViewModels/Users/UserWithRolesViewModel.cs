namespace MovieDG.Core.ViewModels.Users
{
    public class UserWithRolesViewModel
    {
        public IEnumerable<UserViewModel> Admins { get; set; }
        public IEnumerable<UserViewModel> Moderators { get; set; }
        public IEnumerable<UserViewModel> Suports { get; set; }
    }
}
