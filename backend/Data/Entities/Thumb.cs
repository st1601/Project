using Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Thumb : BaseEntity
    {
        [Required]
        public ThumbEnum ThumbType { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int IdeaId { get; set; }

        public User? User { get; set; }

        public Idea? Idea { get; set; }
    }
}
