using API.DTOs.Notification.CreateNotification;
using API.DTOs.Notification.GetNotification;
using Common.DataType;

namespace API.Services.Interfaces
{
    public interface INotificationService
    {
        Task<Response<CreateNotificationResponse>> CreateNotificationAsync(CreateNotificationRequest request);

        Task<Response<GetNotificationResponse>> GetByIdAsync(int id);

        Task<IEnumerable<GetNotificationResponse>> GetAllAsync();
    }
}
