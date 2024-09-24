using NUnit.Framework;

namespace MovieDG.Web.Areas.Identity.IdentityConstants
{
    public static class IdentityErrorMessagesConstants
    {
            public const string UserNullErrorMessage = "Unable to load user with ID '{0}'.";

            public const string ChangeEmailErrorMessage = "There was an error with changing your email.";

            public const string IncorrectUserOrPasswordErrorMessage = "The username or password you typed are incorrect.";

            public const string TwoFactorCodeErrorMessage = "The {0} must be at least {2} and at max {1} characters long.";

            public const string InvalidAuthCodeErrorMessage = "Invalid authenticator code.";

            public const string InvalidRecoveryCodeErrorMessage = "Invalid recovery code entered.";

            public const string UserNameErrorMessage = "Username must start with a capital letter.";

            public const string CountryErrorMessage = "Country must start with a capital letter.";

            public const string CityErrorMessage = "City must start with a capital letter.";

            public const string PasswordErrorMessage = "The {0} must be at least {2} and at max {1} characters long.";

            public const string AlreadyTakenEmailErrorMessage = "This Email address is already taken.";

            public const string PasswordCodeNullErrorMessage = "A code must be supplied for password reset.";

            public const string ConfirmPasswordErrorMessage = "The password and confirmation password do not match.";

            public const string ConfirmEmailErrorMessage = "There was an error with confirming your email. ";

            public const string UnableToLoadTwoFactorAuthUserErrorMessage = "Unable to load two-factor authentication user.";

            public const string NewPasswordConfirmationError = "The new password and confirmation password do not match.";

            public const string NewPasswordErrorMessage = "The new password cannot be the same as the old!";

            public const string IncorrectPasswordErrorMessage = "Incorrect password.";

            public const string DeleteUserErrorMessage = "Unexpected error occurred deleting user.";

            public const string InvalidDisable2FAErrorMessage = "Cannot disable 2FA for user with ID {0} as it's not currently enabled.";
        
            public const string Unexpected2FAErrorMessage = "Unexpected error occurred disabling 2FA for user with ID {0}.";

            public const string EmailUnchangeErrorMessage = "Your email is unchanged.";

            public const string CodeLengthErrorMessage = "The {0} must be at least {2} and at max {1} characters long.";  
        
            public const string InvalidVerificationCodeErrorMessage = "Verification code is invalid.";

            public const string InvalidGenerateCodeErrorMessage = "Cannot generate recovery codes for user because they do not have 2FA enabled.";

            public const string SetUsernameErrorMessage = "Unexpected error when trying to set username.";
    }
}
