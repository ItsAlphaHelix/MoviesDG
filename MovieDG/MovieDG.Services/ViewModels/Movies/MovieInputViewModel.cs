namespace MovieDG.Core.ViewModels.Movies
{
    using System.ComponentModel.DataAnnotations;
    public class MovieInputViewModel
    {
        [Range(0, int.MaxValue, ErrorMessage = "Value cannot be negative !")]
        public int StartIndex { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Value cannot be negative !")]
        public int EndIndex { get; set; }
    }
}
