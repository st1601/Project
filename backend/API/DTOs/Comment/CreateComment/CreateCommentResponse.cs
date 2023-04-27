namespace API.DTOs.Comment.CreateComment
{
    public class CreateCommentResponse
    {
        public CreateCommentResponse(Data.Entities.Comment request)
        {
            CommentConent = request.CommentContent;
            UserName = request.User.UserName;
            Department = request.User.Department.ToString();
            DateSubmitted = request.DateSubmitted;
        }

        public string CommentConent { get; set; }

        public string UserName { get; set; }

        public string Department { get; set; }

        public DateTime DateSubmitted { get; set; }
    }
}
