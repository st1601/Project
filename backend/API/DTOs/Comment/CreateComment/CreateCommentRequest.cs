namespace API.DTOs.Comment.CreateComment
{
    public class CreateCommentRequest
    {
        public string CommentContent { get; set; }

        public int UserId { get; set; }

        public int IdeaId { get; set; }
    }
}
