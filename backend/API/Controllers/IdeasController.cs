using API.DTOs.Idea.CreateIdea;
using API.DTOs.Idea.GetIdea;
using API.DTOs.Idea.UpdateIdea;
using API.Services.Interfaces;
using Common.DataType;
using Common.Constant;
using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Queries;
using API.DTOs.Idea.GetListIdeas;
using API.Queries.Ideas;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IdeasController : ControllerBase
    {
        private readonly IIdeaService _ideaService;

        public IdeasController(IIdeaService ideaService)
        {
            _ideaService = ideaService;
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Staff)]
        public async Task<ActionResult<Response<CreateIdeaResponse>>> Create([FromBody] CreateIdeaRequest request)
        {
            try
            {
                var response = await _ideaService.CreateIdeaAsync(request);

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
        [Authorize(Roles = UserRoles.Staff)]
        public async Task<ActionResult<Response<UpdateIdeaResponse>>> Update([FromBody] UpdateIdeaRequest request)
        {
            try
            {
                var response = await _ideaService.UpdateIdeaAsync(request);

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

        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.Staff)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _ideaService.DeleteIdeaAsync(id);

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

        [HttpGet]
        [Authorize(Roles = UserRoles.Staff)]
        public async Task<ActionResult<GetIdeaResponse>> GetAll()
        {
            try
            {
                var result = await _ideaService.GetAllAsync();

                return Ok(result);
            }
            catch
            {
                return StatusCode(500, ErrorMessages.InternalServerError);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = UserRoles.Staff)]
        public async Task<ActionResult<GetIdeaResponse>> GetById(int id)
        {
            try
            {
                var result = await _ideaService.GetByIdAsync(id);

                if (result == null) return NotFound();

                return Ok(result);
            }
            catch
            {
                return StatusCode(500, ErrorMessages.InternalServerError);
            }
        }

        [HttpGet("pagedlist")]
        [AllowAnonymous]
        public async Task<ActionResult<Response>> GetPagedList([FromQuery] PagingQuery pagingQuery,
                                                                                    [FromQuery] SortQuery sortQuery,
                                                                                    [FromQuery] SearchQuery searchQuery,
                                                                                    [FromQuery] IdeaFilter ideaFilter)
        {
            var request = new GetListIdeasRequest(pagingQuery, sortQuery, searchQuery, ideaFilter);

            try
            {
                var response = await _ideaService.GetPagedListAsync(request);

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

        [HttpGet("most-popular-idea")]
        [AllowAnonymous]
        public async Task<ActionResult<Response>> getIdeasByComments()
        {
            try
            {
                var response = await _ideaService.getTopFiveIdeasByComment();

                if (response == null)
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
