using API.DTOs.Comment.CreateComment;
using API.DTOs.Comment.GetComment;
using API.Services.Interfaces;
using Common.Constant;
using Common.DataType;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<ActionResult<GetCommentResponse>> GetAll()
        {
            try
            {
                var result = await _commentService.GetAllAsync();

                return Ok(result);
            }
            catch
            {
                return StatusCode(500, ErrorMessages.InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCommentResponse>> GetById(int id)
        {
            try
            {
                var result = await _commentService.GetByIdAsync(id);

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
        public async Task<ActionResult<Response<CreateCommentResponse>>> Create([FromBody] CreateCommentRequest request)
        {
            try
            {
                var response = await _commentService.CreateCommentAsync(request);

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
