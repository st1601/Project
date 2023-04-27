using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Idea : BaseEntity
    {
        [Required, MaxLength(225)]
        public string? IdeaTitle { get; set; }

        [Required]
        public string? IdeaDescription { get; set; }

        [Required]
        public DateTime DateSubmitted { get; set; }     

        public string? File { get; set; }

        public string? HashTag { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int EventId { get; set; }

        public User? User { get; set; }

        public Event? Event { get; set; }

        public ICollection<Comment>? Comments { get; set; }

        public ICollection<Thumb>? Thumbs { get; set; }

        public ICollection<Category>? Categories { get; set; }

        public ICollection<Notification>? Notifications { get; set; }
    }
}
