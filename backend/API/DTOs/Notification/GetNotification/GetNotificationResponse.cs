namespace API.DTOs.Notification.GetNotification
{
    public class GetNotificationResponse
    {
        public GetNotificationResponse(Data.Entities.Notification request)
        {
            Id = request.Id;
            NotificationName = request.NotificationName;
            EventName = request.Idea.Event.EventName;
            IdeaTitle = request.Idea.IdeaTitle;
            UserName = request.Idea.User.UserName;
            Department = request.Idea.User.Department.ToString();
        }

        public int Id { get; set; }

        public string NotificationName { get; set; }

        public string EventName { get; set; }

        public string IdeaTitle { get; set; }

        public string UserName { get; set; }

        public string Department { get; set; }
    }
}
