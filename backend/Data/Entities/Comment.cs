using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Comment : BaseEntity
    {
        [Required, MaxLength(225)]
        public string? CommentContent { get; set; }

        [Required]
        public DateTime DateSubmitted { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int IdeaId { get; set; }

        public User? User { get; set; }

        public Idea? Idea { get; set; }
    }
}
