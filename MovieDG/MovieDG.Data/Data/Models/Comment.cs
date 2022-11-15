namespace MoviesDG.Data.Data.Models
{
    using MoviesDG.Data.Constants;
    using System.ComponentModel.DataAnnotations;
    public class Comment
    {
        public Comment()
        {
            this.Comments = new HashSet<MovieComment>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaxCommentLength)]
        public string Content { get; set; }

        public virtual ICollection<MovieComment> Comments { get; set; }
    }
}
