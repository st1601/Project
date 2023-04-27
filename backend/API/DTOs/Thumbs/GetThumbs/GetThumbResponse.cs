
using Common.Enums;

namespace API.DTOs.Thumbs.GetThumbs
{
    public class GetThumbResponse
    {
        public GetThumbResponse(string ideaTitle, int thumbUp, int thumbDown)
        {
            this.IdeaTitle = ideaTitle;
            this.ThumbUp = thumbUp;
            this.ThumbDown = thumbDown;
        }

        public string IdeaTitle { get; set; }

        public int ThumbUp { get; set; }

        public int ThumbDown { get; set;}
    }
}
