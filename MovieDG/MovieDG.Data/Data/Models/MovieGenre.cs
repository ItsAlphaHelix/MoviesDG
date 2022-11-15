namespace MovieDG.Data.Data.Models
{
    using MoviesDG.Data.Models;
    using System.ComponentModel.DataAnnotations.Schema;

    public class MovieGenre
    {
        [ForeignKey(nameof(Movie))]
        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        [ForeignKey(nameof(Genre))]
        public int GenreId { get; set; }

        public Genre Genre { get; set; }
    }
}
