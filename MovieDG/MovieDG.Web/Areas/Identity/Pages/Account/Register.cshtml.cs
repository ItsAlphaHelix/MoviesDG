﻿namespace MovieDG.Web.Areas.Identity.Pages.Account
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using MovieDG.Data.Data.Models;
    using MovieDG.Web.Areas.Identity.IdentityConstants;
    using MoviesDG.Data.Repositories;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using System.Text.Encodings.Web;
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<RegisterModel> logger;
        private readonly IEmailSender emailSender;
        private readonly IRepository<ApplicationUser> usersRepository;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IRepository<ApplicationUser> usersRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
            this.usersRepository = usersRepository;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [RegularExpression(IdentityMessageConstants.StartWithCapitalLetterRegex, ErrorMessage = IdentityErrorMessagesConstants.UserNameErrorMessage)]
            public string Username { get; set; }

            [Required]
            [RegularExpression(IdentityMessageConstants.StartWithCapitalLetterRegex, ErrorMessage = IdentityErrorMessagesConstants.CountryErrorMessage)]
            public string Country { get; set; }

            [Required]
            [RegularExpression(IdentityMessageConstants.StartWithCapitalLetterRegex, ErrorMessage = IdentityErrorMessagesConstants.CityErrorMessage)]
            public string City { get; set; }

            [Required]
            [Phone]
            public string PhoneNmber { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = IdentityErrorMessagesConstants.PasswordErrorMessage, MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Compare(nameof(Password), ErrorMessage = IdentityErrorMessagesConstants.ConfirmPasswordErrorMessage)]
            public string ConfirmPassword { get; set; }
        }


        public async Task OnGetAsync(string? returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl ??= this.Url.Content("~/");

            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (this.ModelState.IsValid)
            {
                if (this.usersRepository.AllAsNoTracking().Any(x => x.Email.ToLower() == this.Input.Email.ToLower()))
                {
                    this.ModelState.AddModelError(string.Empty, IdentityErrorMessagesConstants.AlreadyTakenEmailErrorMessage);
                }
                else if (this.usersRepository.AllAsNoTracking().Any(x => x.UserName.ToLower() == this.Input.Username.ToLower()))
                {
                    this.ModelState.AddModelError(string.Empty, IdentityMessageConstants.AlreadyTakenUsernameMessage);
                }
                else
                {
                    var user = new ApplicationUser { UserName = this.Input.Username, Email = this.Input.Email, Country = this.Input.Country, City = this.Input.City, PhoneNumber = this.Input.PhoneNmber };
                    var result = await this.userManager.CreateAsync(user, this.Input.Password);

                    if (result.Succeeded)
                    {
                        this.logger.LogInformation(IdentityMessageConstants.UserCreateNewAccountWithPassMessage);
                        //await this.userManager.AddToRoleAsync(user, GlobalConstants.BasicUserRoleName);

                        var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = this.Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                            protocol: this.Request.Scheme);

                        await this.emailSender.SendEmailAsync(
                            this.Input.Email,
                            "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        if (this.userManager.Options.SignIn.RequireConfirmedAccount)
                        {
                            return this.RedirectToPage("RegisterConfirmation", new { email = this.Input.Email, returnUrl = returnUrl });
                        }
                        else
                        {
                            await this.signInManager.SignInAsync(user, isPersistent: false);
                            return this.LocalRedirect(returnUrl);
                        }
                    }

                    foreach (var error in result.Errors)
                    {
                        this.ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }
    }
}
