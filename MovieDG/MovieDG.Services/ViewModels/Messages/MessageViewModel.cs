namespace MovieDG.Core.ViewModels.Messages
{
    using System.ComponentModel.DataAnnotations;
    public class MessageViewModel
    {
        [Required]
        public string FromName { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
