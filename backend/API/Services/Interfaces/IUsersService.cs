using API.DTOs.User.StatisticalUser;
using API.DTOs.User.Authentication;
using API.DTOs.User.ChangePassword;
using API.DTOs.User.CreateUser;
using API.DTOs.User.GetListUsers;
using API.DTOs.User.GetUser;
using API.DTOs.User.UpdateUser;
using API.DTOs.Department;
using Common.DataType;

namespace API.Services.Interfaces
{
    public interface IUsersService
    {
        Task<LoginResponse?> LoginUserAsync(LoginRequest request);

        Task<Response<GetUserResponse>> GetByIdAsync(int id);

        Task<IEnumerable<GetUserResponse>> GetAllAsync();

        Task<Response<GetListUsersResponse>> GetPagedListAsync(GetListUsersRequest request);

        Task<Response<CreateUserResponse>> CreateUserAsync(CreateUserRequest request);

        Task<Response<UpdateUserResponse>> UpdateUserAsync(UpdateUserRequest request);

        Task<bool> DeleteUserAsync(int id);

        Task<Response> ChangePasswordAsync(ChangePasswordRequest request);

        Task<Response<StatisticalUserResponse>> countEmployee();

        Task<Response<StatisticalDepartmentResponse>> countDepartment();
    }
}
