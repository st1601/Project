namespace API.DTOs.Notification.CreateNotification
{
    public class CreateNotificationResponse
    {
        public CreateNotificationResponse(Data.Entities.Notification request)
        {
            NotificationName = request.NotificationName;
            EventName = request.Idea.Event.EventName;
            IdeaTitle = request.Idea.IdeaTitle;
            UserName = request.Idea.User.UserName;
            Department = request.Idea.User.Department.ToString();
        }

        public string NotificationName { get; set; }

        public string EventName { get; set; }

        public string IdeaTitle { get; set; }

        public string UserName { get; set; }

        public string Department { get; set; }
    }
}
