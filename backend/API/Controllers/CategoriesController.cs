using API.DTOs.Category.CreateCategory;
using API.DTOs.Category.GetCategory;
using API.Services.Interfaces;
using Common.DataType;
using Common.Constant;
using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.DTOs.Category.GetListCategories;
using API.Queries;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.QAManager)]
        public async Task<ActionResult<Response<CreateCategoryResponse>>> Create([FromBody] CreateCategoryRequest request)
        {
            try
            {
                var response = await _categoryService.CreateCategoryAsync(request);

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
        [Authorize(Roles = UserRoles.QAManager)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _categoryService.DeleteCategoryAsync(id);

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

        [HttpGet("{id}")]
        [Authorize(Roles = "Staff, QAManager")]
        public async Task<ActionResult<Response<GetCategoryResponse>>> GetById(int id)
        {
            try
            {
                var result = await _categoryService.GetByIdAsync(id);

                if(result == null)
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

        [HttpGet]
        [Authorize(Roles = "Staff, QAManager")]
        public async Task<ActionResult<GetCategoryResponse>> GetAll()
        {
            try
            {
                var result = await _categoryService.GetAllAsync();

                return Ok(result);
            }
            catch
            {
                return StatusCode(500, ErrorMessages.InternalServerError);
            }
        }

        [HttpGet("pagedlist")]
        [Authorize(Roles = "Staff, QAManager")]
        public async Task<ActionResult<GetListCategoriesResponse>> GetPagedList([FromQuery] PagingQuery pagingQuery,
                                                                            [FromQuery] SearchQuery searchQuery,
                                                                            [FromQuery] SortQuery sortQuery)
        {
            var request = new GetListCategoriesRequest(pagingQuery, sortQuery, searchQuery);

            try
            {
                var response = await _categoryService.GetPagedListAsync(request);

                if(!response.IsSuccess)
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
