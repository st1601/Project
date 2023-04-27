namespace API.DTOs.Idea.GetIdea
{
    public class GetTopIdeaResponse
    {

        public GetTopIdeaResponse(GetIdeaResponse idea, int commentCount)
        {
            this.idea = idea;
            this.commentCount = commentCount;
        }

        public GetIdeaResponse idea { get; set; }

        public int commentCount { get; set; }
    }
}
