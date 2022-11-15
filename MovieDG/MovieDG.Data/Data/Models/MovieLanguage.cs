namespace MovieDG.Data.Data.Models
{
    using MoviesDG.Data.Models;
    using System.ComponentModel.DataAnnotations.Schema;

    public class MovieLanguage
    {
        [ForeignKey(nameof(Movie))]
        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        [ForeignKey(nameof(Language))]
        public int LanguageId { get; set; }

        public Language Language { get; set; }
    }
}
