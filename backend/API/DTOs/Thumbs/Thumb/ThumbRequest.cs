using Common.Enums;

namespace API.DTOs.Thumbs.Thumb
{
    public class ThumbRequest
    {
        public ThumbEnum ThumbType { get; set; }
        public int UserId { get; set;}
        public int IdeaId { get; set;}
    }
}
