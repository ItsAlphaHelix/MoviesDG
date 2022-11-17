namespace MovieDG.Core.ViewModels.Movies
{
    public class MovieViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Poster { get; set; }

        public string Trailer { get; set; }

        public double Popularity { get; set; }

        public double AverageVotes { get; set; }

        public int TotalVotes { get; set; }
    }
}
