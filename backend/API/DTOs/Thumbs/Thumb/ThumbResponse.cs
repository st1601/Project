using Common.Enums;

namespace API.DTOs.Thumbs.Thumb
{
    public class ThumbResponse
    {
        public ThumbResponse(Data.Entities.Thumb request) 
        {
            ThumbType = request.ThumbType;
            UserId = request.UserId;
            IdeaId = request.IdeaId;
        }

        public ThumbEnum ThumbType { get; set; }
        public int UserId { get; set;}
        public int IdeaId { get; set;}
    }
}
