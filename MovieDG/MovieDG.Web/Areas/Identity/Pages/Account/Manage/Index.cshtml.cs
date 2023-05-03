namespace MovieDG.Web.Areas.Identity.Pages.Account.Manage
{
    using AspNetCoreHero.ToastNotification.Abstractions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using MovieDG.Data.Data.Models;
    using MovieDG.Web.Areas.Identity.IdentityConstants;
    using MoviesDG.Data.Constants;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly INotyfService toastNotification;
        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            INotyfService toastNotification)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.toastNotification = toastNotification;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            private const int MaxLengthCity = 30;
            private const int MinLengthCity = 5;

            [Required]
            [StringLength(ValidationConstants.MaxUserName, MinimumLength = ValidationConstants.MinUserName)]
            public string Username { get; set; }

            [Required]
            [StringLength(ValidationConstants.MaxCountryName, MinimumLength = ValidationConstants.MinCountryName)]
            public string Country { get; set; } 

            [Required]
            [StringLength(MaxLengthCity, MinimumLength = MinLengthCity)]
            public string City { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await this.userManager.GetUserNameAsync(user);
            var country = user.Country;
            var city = user.City;

            Input = new InputModel
            {
                Username = userName,
                Country = country,
                City = city,
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this.userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(String.Format(IdentityErrorMessagesConstants.UserNullErrorMessage, user.Id));
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await this.userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(String.Format(IdentityErrorMessagesConstants.UserNullErrorMessage, user.Id));
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var userName = await this.userManager.GetUserNameAsync(user);
            if (this.Input.Username != userName)
            {
                var setUsername = await this.userManager.SetUserNameAsync(user, this.Input.Username);

                if (!setUsername.Succeeded)
                {
                    this.StatusMessage = IdentityErrorMessagesConstants.SetUsernameErrorMessage;
                    return this.RedirectToPage();
                }
            }

            var country = user.Country;
            if (this.Input.Country != country)
            {
                user.Country = this.Input.Country;
                await this.userManager.UpdateAsync(user);
            }

            var city = user.City;
            if (this.Input.City != city)
            {
                user.City = this.Input.City;
                await this.userManager.UpdateAsync(user);
            }

            await this.signInManager.RefreshSignInAsync(user);
            this.toastNotification.Success(IdentityMessageConstants.SuccessfullyUpdateUserProfile);
            return RedirectToPage();
        }
    }
}
