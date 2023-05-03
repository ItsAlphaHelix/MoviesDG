namespace MovieDG.Core.ViewModels.Movies
{
    using System.ComponentModel.DataAnnotations;
    using static MovieDG.Core.ErrorMessages.ErrorMessageConstants;
    public class MovieInputViewModel
    {
        [Range(0, int.MaxValue, ErrorMessage = MovieError)]
        public int StartIndex { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = MovieError)]
        public int EndIndex { get; set; }
    }
}
