namespace API.DTOs.Notification.CreateNotification
{
    public class CreateNotificationRequest
    {
        public string? NotificationName { get; set; }

        public int IdeaId { get; set; }
    }
}
