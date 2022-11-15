namespace MoviesDG.Data.Models
{
    using MovieDG.Data.Data.Models;
    using MoviesDG.Data.Constants;
    using System.ComponentModel.DataAnnotations;
    public class Movie
    {
        public Movie()
        {
            this.MovieGenres = new HashSet<MovieGenre>();

            this.MovieActors = new HashSet<MovieActor>();

            this.MovieLanguages = new HashSet<MovieLanguage>();

            this.MovieCountries = new HashSet<MovieCountry>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaxMovieTitle)]
        public string Title { get; set; } = null!;

        [Required]
        public string Poster { get; set; } = null!;

        [Required]
        public string Banner { get; set; }

        [Required]
        public string Overview { get; set; } = null!;

        [MaxLength(ValidationConstants.TrailerMaxLength)]
        public string Trailer { get; set; }

        public DateTime ReleaseDate { get; set; }

        [MaxLength(ValidationConstants.IMDBLinkMaxLength)]
        public string IMDBLink { get; set; }

        public int TMDBId { get; set; }

        public int Runtime { get; set; }

        public double Popularity { get; set; }

        public double AverageVotes { get; set; }

        public int TotalVotes{ get; set; }

        public virtual ICollection<MovieGenre> MovieGenres { get; set; }

        public virtual ICollection<MovieActor> MovieActors { get; set; } 

        public virtual ICollection<MovieLanguage> MovieLanguages { get; set; } 

        public virtual ICollection<MovieCountry> MovieCountries { get; set; }
    }
}
