namespace MovieDG.Core.ViewModels.Movies
{
    public class HomepageViewModel
    {
        public BannerMovieViewModel LatestMovie { get; set; }

        public IEnumerable<MovieViewModel> NewMovies { get; set; }
    }
}
