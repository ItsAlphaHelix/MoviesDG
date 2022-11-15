namespace MoviesDG.Data.Models
{
    using MovieDG.Data.Data.Models;
    using MoviesDG.Data.Constants;
    using System.ComponentModel.DataAnnotations;
    public class Genre
    {
        public Genre()
        {
            this.Movies = new HashSet<MovieGenre>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaxGenreType)]
        public string Type { get; set; } = null!;

        public virtual ICollection<MovieGenre> Movies { get; set; }
    }
}