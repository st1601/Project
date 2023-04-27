using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Notification : BaseEntity
    {
        [Required, MaxLength(225)]
        public string? NotificationName { get; set; }

        [Required]
        public int IdeaId { get; set; }

        public Idea? Idea { get; set; }
    }
}