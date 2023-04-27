namespace API.DTOs.Idea.UpdateIdea
{
    public class UpdateIdeaRequest
    {
        public int Id { get; set; }

        public string IdeaTitle { get; set; }

        public string IdeaDescription { get; set; }

        public string File { get; set; }

        public string HashTag { get; set; }

        public int UserId { get; set; }

        public int EventId { get; set; }

        public List<int> CategoryIds { get; set; } = null!;
    }
}
