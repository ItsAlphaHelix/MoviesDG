namespace MovieDG.Core.ViewModels.Movies
{
    using MovieDG.Core.ViewModels.Actors;
    using MovieDG.Core.ViewModels.Countries;
    using MovieDG.Core.ViewModels.Genres;
    public class MovieDetailsViewModel : MovieViewModel
    {
        public string Overview { get; set; }

        public int Runtime { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Languages { get; set; }

        public IEnumerable<GenreViewModel> Genres { get; set; }

        public IEnumerable<ActorViewModel> Actors { get; set; }

        public IEnumerable<CountryViewModel> Countries { get; set; }
    }
}
