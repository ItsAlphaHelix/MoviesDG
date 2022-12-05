﻿namespace MovieDG.Core.ViewModels.Contact
{
    using System.ComponentModel.DataAnnotations;
    using static MoviesDG.Data.Constants.ValidationConstants;
    using static MovieDG.Core.ErrorMessages;
    public class ContactInputViewModel
    {
        public int ContactId { get; set; }

        [Required]
        [StringLength(MaxContactName, MinimumLength = MinContactName, ErrorMessage = NameError)]
        public string Name { get; set; } = null!;

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(MaxSubjectLength, MinimumLength = MinSubjectLength, ErrorMessage = SubjectContactError)]
        public string Subject { get; set; } = null!;

        [Required]
        [StringLength(MaxMessageLength, MinimumLength = MinMessageLength, ErrorMessage = MessageError)]
        public string Message { get; set; } = null!;
    }
}
