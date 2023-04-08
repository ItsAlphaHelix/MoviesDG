using System.ComponentModel.DataAnnotations.Schema;

namespace MovieDG.Data.Data.Models
{
    public class Message
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string Name { get; set; }

    }
}