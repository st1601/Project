namespace API.DTOs.Comment.GetComment
{
    public class GetCommentResponse
    {
        public GetCommentResponse(Data.Entities.Comment request)
        {
            Id = request.Id;
            CommentConent = request.CommentContent;
            UserName = request.User.UserName;
            Department = request.User.Department.ToString();
            DateSubmitted = request.DateSubmitted;
        }
        
        public int Id { get; set; }

        public string CommentConent { get; set; }

        public string UserName { get; set; }

        public string Department { get; set; }

        public DateTime DateSubmitted { get; set; }
    }
}
