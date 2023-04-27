using API.DTOs.User.CreateUser;
using API.DTOs.User.GetListUsers;
using API.DTOs.User.GetUser;
using API.DTOs.User.UpdateUser;
using API.Queries;
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
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<Response<CreateUserResponse>>> Create([FromBody] CreateUserRequest request)
        {
            try
            {
                var response = await _usersService.CreateUserAsync(request);

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
        public async Task<ActionResult<Response<UpdateUserResponse>>> Update([FromBody] UpdateUserRequest request)
        {
            try
            {
                var response = await _usersService.UpdateUserAsync(request);

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
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _usersService.DeleteUserAsync(id);

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
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<GetUserResponse>> GetAll()
        {
            try
            {
                var result = await _usersService.GetAllAsync();

                return Ok(result);
            }
            catch
            {
                return StatusCode(500, ErrorMessages.InternalServerError);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<Response<GetUserResponse>>> GetById(int id)
        {
            try
            {
                var result = await _usersService.GetByIdAsync(id);

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

        [HttpGet("pagedlist")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<Response<GetUserResponse>>> GetPagedList([FromQuery] PagingQuery pagingQuery,
                                                                                    [FromQuery] SortQuery sortQuery,
                                                                                    [FromQuery] FilterQuery filterQuery,
                                                                                    [FromQuery] SearchQuery searchQuery)
        {
            var request = new GetListUsersRequest(pagingQuery, sortQuery, filterQuery, searchQuery);

            try
            {
                var response = await _usersService.GetPagedListAsync(request);

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
