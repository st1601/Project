using API.Services.Interfaces;
using Common.DataType;
using Common.Constant;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class StatisticalController : ControllerBase
    {
        private readonly IUsersService _usersService;

        private readonly ICategoryService _categoryService;

        private readonly IIdeaService _ideaService;

        public StatisticalController(IUsersService usersService, ICategoryService categoryService, IIdeaService ideaService)
        {
            _usersService = usersService;
            _categoryService = categoryService;
            _ideaService = ideaService;
        }

        [HttpGet("for-ideas")]
        [Authorize(Roles = "QAManager")]
        public async Task<ActionResult<Response>> statisticalForIdeas()
        {
            try
            {
                var response = await _ideaService.countIdeas();

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


        [HttpGet("for-departments")]
        [Authorize(Roles = "QAManager")]
        public async Task<ActionResult<Response>> statisticalForDepartments()
        {
            try
            {
                var response = await _usersService.countDepartment();

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

        [HttpGet("for-employees")]
        [Authorize(Roles = "QAManager")]
        public async Task<ActionResult<Response>> statisticalForEmployees()
        {
            try
            {
                var response = await _usersService.countEmployee();

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

        [HttpGet("for-catalogs")]
        [Authorize(Roles = "QAManager")]
        public async Task<ActionResult<Response>> statisticalForCatalogs()
        {
            try
            {
                var response = await _categoryService.countCatalog();

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

        [HttpGet("top-five-ideas-by-comments")]
        [Authorize(Roles = "QAManager")]
        public async Task<ActionResult<Response>> getTopFiveIdeasByComments()
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
