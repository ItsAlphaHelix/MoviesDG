namespace MovieDG.Core.ViewModels.Users
{
    using MovieDG.Core.ErrorMessages;
    using System.ComponentModel.DataAnnotations;
    public class UserViewModel
    {
        private const string StartWithCapitalLetterRegex = @"[A-Z]{1}[\w]+";

        [Required]
        [RegularExpression(StartWithCapitalLetterRegex, ErrorMessage = ErrorMessageConstants.UserNameErrorMessage)]
        public string UserName { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
