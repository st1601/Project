using API.DTOs.Comment.CreateComment;
using API.DTOs.Notification.CreateNotification;
using API.DTOs.Notification.GetNotification;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using Common.Constant;
using Common.DataType;
using Data.Entities;

namespace API.Services.Implements
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        private readonly IIdeaRepository _ideaRepository;

        public NotificationService(INotificationRepository notificationRepository, IIdeaRepository ideaRepository)
        {
            _notificationRepository = notificationRepository;
            _ideaRepository = ideaRepository;
        }
        public async Task<Response<CreateNotificationResponse>> CreateNotificationAsync(CreateNotificationRequest request)
        {
            using (var trasaction = _notificationRepository.DatabaseTransaction())
            {
                try
                {
                    var idea = await _ideaRepository.GetAsync(idea => idea.Id == request.IdeaId);

                    if (idea == null)
                    {
                        return new Response<CreateNotificationResponse>(false, ErrorMessages.NotFound);
                    }

                    var newEntity = new Notification
                    {
                        NotificationName = request.NotificationName,
                        IdeaId = request.IdeaId,
                    };

                    var newNotification = _notificationRepository.Create(newEntity);

                    _notificationRepository.SaveChanges();

                    trasaction.Commit();

                    var responseData = new CreateNotificationResponse(newNotification);

                    return new Response<CreateNotificationResponse>(true, Messages.ActionSuccess, responseData);
                }
                catch
                {
                    trasaction.Rollback();

                    return new Response<CreateNotificationResponse>(false, ErrorMessages.BadRequest);
                }
            }
        }

        public async Task<IEnumerable<GetNotificationResponse>> GetAllAsync()
        {
            return( await _notificationRepository.GetAllAsync())
                .Select(notification => new GetNotificationResponse(notification));
        }

        public async Task<Response<GetNotificationResponse>> GetByIdAsync(int id)
        {
            var notification = await _notificationRepository.GetAsync(n => n.Id == id);

            if(notification== null)
            {
                return new Response<GetNotificationResponse>(false, ErrorMessages.NotFound);
            }

            var responseData = new GetNotificationResponse(notification);

            return new Response<GetNotificationResponse>(true, Messages.ActionSuccess, responseData);
        }
    }
}
