using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Event : BaseEntity
    {
        [Required, MaxLength(225)]
        public string? EventName { get; set; }

        [Required]
        public string? EventDescription { get; set; }

        [Required]
        public DateTime FirstClosingDate { get; set; }

        [Required]
        public DateTime LastClosingDate { get; set; }

        [Required]
        public int UserId { get; set; }

        public User? User { get; set; }

        public ICollection<Idea>? Ideas { get; set; }
    }
}
