namespace MovieDG.Web.Areas.Identity.IdentityConstants
{
    public static class IdentityMessageConstants
    {
        public const string SuccessfullyConfirmEmailMessage = "Thank you for confirming your email. Now you can log in to your account.";

        public const string SuccessfullyChangedEmailMessage = "You have successfully changed your email address.";

        public const string ConfirmEmailMessage = "Thank you for confirming your email.";

        public const string LoginUserMessage = "User logged in.";

        public const string UserAccountLockMessage = "User account locked out.";

        public const string LoginWithTwoFactorAuthMessage = "User with ID {0} logged in with 2fa.";

        public const string LockedoutAccountMessage = "User with ID {0} account locked out.";

        public const string InvalidAuthCodeMessage = "Invalid authenticator code entered for user with ID {0}.";

        public const string LoginWithRecoveryCodeMessage = "User with ID {0} logged in with a recovery code.";

        public const string InvalidRecoveryCodeMessage = "Invalid recovery code entered for user with ID {0} ";

        public const string UserLogoutMessage = "User logged out.";

        public const string StartWithCapitalLetterRegex = @"[A-Z]{1}[\w]+";

        public const string AlreadyTakenUsernameMessage = "This Username is already taken.";

        public const string UserCreateNewAccountWithPassMessage = "User created a new account with password.";

        public const string VerificationEmailSuccessfullySentMessage = "Verification email sent. Please check your email.";

        public const string SuccessfullyUserChangePasswordLogMessage = "User changed their password successfully.";

        public const string SuccessfullyUserChangePasswordMessage = "Your password has been changed.";

        public const string DeleteUserLogMessage = "User with ID {0} deleted themselves.";

        public const string SuccessfullyDisabled2FALogMessage = "User with ID {0} has disabled 2fa.";

        public const string SuccessfullyDisabled2FAMessage = "2fa has been disabled. You can reenable 2fa when you setup an authenticator app";

        public const string ConfirmLinkToChangeEmailSentMessage = "Confirmation link to change email sent. Please check your email.";

        public const string VerificationEmailSentMessage = "To change your email, you should verify new email in your inbox";

        public const string Enable2FAWithAppLogMessage = "User with ID {0} has enabled 2FA with an authenticator app.";

        public const string SuccessfullyVerifiedAuthAppMessage = "Your authenticator app has been verified.";

        public const string WebAppName = "MovieDG.Web";

        public const string SuccessfullyGenerateCodeLogMessage = "User with ID {0} has generated new 2FA recovery codes.";

        public const string SuccessfullyGenerateCodeMessage = "You have generated new recovery codes.";

        public const string SuccessfullyUpdateUserProfile = "Your profile has been updated.";

        public const string SuccessfullyResetAuthAppKeyLogMessage = "User with ID {0} has reset their authentication app key.";

        public const string SuccessfullyResetAuthAppKeyMessage = "Your authenticator app key has been reset, you will need to configure your authenticator app using the new key.";

        public const string ForgottenBrowserMessage = "The current browser has been forgotten. When you login again from this browser you will be prompted for your 2fa code.";
    }
}
