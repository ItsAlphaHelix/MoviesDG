namespace MovieDG.Data.Data.Models
{
    using MoviesDG.Data.Models;
    using System.ComponentModel.DataAnnotations.Schema;

    public class MovieCountry
    {
        [ForeignKey(nameof(Movie))]
        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }

        public Country Country { get; set; }
    }
}
