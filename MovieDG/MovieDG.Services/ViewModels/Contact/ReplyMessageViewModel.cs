namespace MovieDG.Core.ViewModels.Contact
{
    using System.ComponentModel.DataAnnotations;
    using static MoviesDG.Data.Constants.ValidationConstants;
    using static MovieDG.Core.ErrorMessages;
    public class ReplyMessageViewModel
    {
        [Required]
        public string AdminId { get; set; } = null!;

        [Required]
        [StringLength(MaxContactName, MinimumLength = MinContactName, ErrorMessage = NameError)]
        public string Username { get; set; } = null!;

        [Required]
        [StringLength(MaxMessageLength, MinimumLength = MinMessageLength, ErrorMessage = MessageError)]
        public string ReplyMessage { get; set; } = null!;
    }
}
