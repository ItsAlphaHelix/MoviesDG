namespace MovieDG.Core.ViewModels.Movies
{
    public class HomepageViewModel
    {
        public BannerHomeMovieViewModel LatestMovie { get; set; }

        public IEnumerable<MovieViewModel> NewMovies { get; set; }
    }
}
