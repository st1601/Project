using API.DTOs.Thumbs.GetThumbs;
using API.DTOs.Thumbs.Thumb;
using API.Services.Interfaces;
using Common.Constant;
using Common.DataType;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ThumbsController : ControllerBase
    {
        private readonly IThumbsService _thumbServices;

        public ThumbsController(IThumbsService thumbservices)
        {
            _thumbServices = thumbservices;
        }

        [HttpPost]
        public async Task<ActionResult<Response<ThumbResponse>>> Create([FromBody] ThumbRequest request)
        {
            try
            {
                var response = await _thumbServices.CreateThumbAsync(request);

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

        [HttpGet("for-idea/{id}")]
        public async Task<ActionResult<Response<GetThumbResponse>>> GetThumbsByIdeaIdAsync(int id) 
        {
            try
            {
                var response = await _thumbServices.CountThumbsByIdeaIdAsync(id);

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

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _thumbServices.DeleteThumbAsync(id);

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
    }
}
