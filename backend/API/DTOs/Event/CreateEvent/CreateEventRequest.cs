using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Event.CreateEvent
{
    public class CreateEventRequest
    {
        [Required]
        public string? EventName { get; set; }

        [Required]
        public string? EventDescription { get; set; }

        [Required]
        public DateTime FirstClosingDate { get; set; }

        [Required]
        public DateTime LastClosingDate { get; set; }

        public int UserId { get; set; }
    }
}
