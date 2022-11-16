namespace MovieDG.Services.Models.Movies
{
    public class MovieViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Poster { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int Runtime { get; set; }

        public double Popularity { get; set; }

        public double AverageVotes { get; set; }

        public int TotalVotes { get; set; }
    }
}
