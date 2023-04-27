using API.DTOs.Notification.CreateNotification;
using API.DTOs.Notification.GetNotification;
using API.Services.Interfaces;
using Common.Constant;
using Common.DataType;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<ActionResult<GetNotificationResponse>> GetAll()
        {
            try
            {
                var result = await _notificationService.GetAllAsync();

                return Ok(result);
            }
            catch
            {
                return StatusCode(500, ErrorMessages.InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetNotificationResponse>> GetById (int id)
        {
            try
            {
                var result = await _notificationService.GetByIdAsync(id);

                if (result == null)
                {
                    return NotFound(result);
                }

                return Ok(result);
            }
            catch
            {
                return StatusCode(500, ErrorMessages.InternalServerError);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Response<CreateNotificationResponse>>> Create([FromBody] CreateNotificationRequest request)
        {
            try
            {
                var response = await _notificationService.CreateNotificationAsync(request);

                if (!response.IsSuccess)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch
            {
                return StatusCode(500, ErrorMessages.InternalServerError);
            }
        }
    }
}
