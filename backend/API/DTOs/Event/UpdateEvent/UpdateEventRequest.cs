using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Event.UpdateEvent
{
    public class UpdateEventRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime FirstClosingDate { get; set; }

        [Required]
        public DateTime LastClosingDate { get; set; }
    }
}
