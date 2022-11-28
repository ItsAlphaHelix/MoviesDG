namespace MovieDG.Web.Areas.Identity.Pages.Account.Manage
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using MovieDG.Data.Data.Models;
    using MoviesDG.Data.Models;

    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Username { get; set;}

            public string Country { get; set; }

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
                return NotFound($"Unable to load user with ID '{this.userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await this.userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{this.userManager.GetUserId(User)}'.");
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
                    this.StatusMessage = "Unexpected error when trying to set username.";
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
            this.StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
