namespace MoviesDG.Data.Data.Models
{
    using MoviesDG.Data.Models;
    using System.ComponentModel.DataAnnotations.Schema;

    public class MovieComment
    {
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        //[Required]
        //public string UserId { get; set; }

        //public virtual ApplicationUser User { get; set; }

        [ForeignKey(nameof(Comment))]
        public int? CommentId { get; set; }

        public virtual Comment Comment { get; set; }
    }
}