namespace API.DTOs.Idea.CreateIdea
{
    public class CreateIdeaRequest
    {
        public string IdeaTitle { get; set; }

        public string IdeaDescription { get; set; }

        public string File { get; set; }

        public string HashTag { get; set; }

        public int UserId { get; set; }

        public int EventId { get; set; }

        public List<int> CategoryIds { get; set; } = null!;
    }
}
