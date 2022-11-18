using MovieDG.Core.ViewModels.Genres;

namespace MovieDG.Core.ViewModels.Movies
{
    public class BannerMovieViewModel
    {
        public string Title { get; set; }

        public string Trailer { get; set; }

        public string Banner { get; set; }

        public int Runtime { get; set; }

        public DateTime ReleaseDate { get; set; }

        public IEnumerable<GenreViewModel> Genres { get; set; }
    }
}
