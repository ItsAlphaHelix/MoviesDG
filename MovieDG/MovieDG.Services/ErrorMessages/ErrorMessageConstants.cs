namespace MovieDG.Core.ErrorMessages
{
    public static class ErrorMessageConstants
    {
        //Contact Message Errors

        public const string ContactNameError = "{0} should be between {2} and {1} words.";

        public const string SubjectContactError = "{0} should be between {2} and {1} words.";

        public const string ContactMessageError = "{0} should be between {2} and {1} words.";

        public const string ContactSubmissionErrorMessage = "The submision can not be null.";

        public const string ContactNullErrorMessage = "The contact can not be null.";

        //Movies Message Errors

        public const string MovieError = "Value cannot be negative.";

        public const string MovieNullErrorMessage = "The movie can not be null.";

        public const string InvalidMovieUserErrorMessage = "Invalid User ID.";

        public const string InvalidMovieErrorMessage = "Invalid Movie ID.";

        //Email Message Error

        public const string EmailSubjectErrorMessage = "Subject and message should be provided.";

        //Data Service Exceptions

        public const string HttpRequestExceptionErrorMessage = "An error occurred.";

        public const string NotSupportedException = "The content type is not supported.";

        public const string JsonExceptionErrorMessage = "Invalid JSON.";

        //User Message Exceptions

        public const string UserNameErrorMessage = "Username must start with a capital letter.";

        public const string UserNotFound = "User not found.";

        public const string AlreadyAssignedRoleToUser = "The user is already assigned to the {0} role.";

        //Role Message Exceptions

        public const string RoleDoesNotExist = "The role doesn't exists.";

        public const string MemberRoleCanNotBeNull = "The member role can not be null.";

        public const string AdminRoleCanNotBeNull = "The admin role can not be null.";

        public const string SuportRoleCanNotBeNull = "The suport role can not be null.";
    }
}
