namespace MovieDG.Data.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Message
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string Name { get; set; }

    }
}