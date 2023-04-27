using API.DTOs.Event.CreateEvent;
using API.DTOs.Event.GetEvent;
using API.DTOs.Event.GetListEvents;
using API.DTOs.Event.UpdateEvent;
using API.Queries;
using API.Queries.Events;
using API.Services.Interfaces;
using Common.Constant;
using Common.DataType;
using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController (IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<GetEventResponse>> GetAll()
        {
            try
            {
                var result = await _eventService.GetAllAsync();

                return Ok(result);
            }
            catch
            {
                return StatusCode(500, ErrorMessages.InternalServerError);
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Response<GetEventResponse>>> GetById(int id)
        {
            try
            {
                var result = await _eventService.GetByIdAsync(id);

                if (result == null) return NotFound(result);

                return Ok(result);
            }
            catch
            {
                return StatusCode(500, ErrorMessages.InternalServerError);
            }
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<Response<CreateEventResponse>>> Create([FromBody] CreateEventRequest request)
        {
            try
            {
                var response = await _eventService.CreateEventAsync(request);

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

        [HttpPut]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<Response<UpdateEventResponse>>> Update([FromBody] UpdateEventRequest request)
        {
            try
            {
                var response = await _eventService.UpdateEventAsync(request);

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

        [HttpDelete]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _eventService.DeleteEventAsync(id);

                if (!result)
                {
                    return BadRequest(ErrorMessages.NotFound);
                }

                return Ok(Messages.ActionSuccess);
            }
            catch
            {
                return StatusCode(500, ErrorMessages.InternalServerError);
            }
        }

        [HttpGet("pagedlist")]
        [AllowAnonymous]
        public async Task<ActionResult<Response<GetListEventsResponse>>> GetPagedList([FromQuery] PagingQuery pagingQuery,
                                                                                [FromQuery] SearchQuery searchQuery,
                                                                                [FromQuery] SortQuery sortQuery,
                                                                                [FromQuery] EventFilter eventFilter)
        {
            var request = new GetListEventsRequest(pagingQuery, sortQuery, searchQuery, eventFilter);

            try
            {
                var response = await _eventService.GetPagedListAsync(request);

                if (!response.IsSuccess)
                {
                    return NotFound(response);
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
