using API.DTOs.Department;
using API.DTOs.User.Authentication;
using API.DTOs.User.ChangePassword;
using API.DTOs.User.CreateUser;
using API.DTOs.User.GetListUsers;
using API.DTOs.User.GetUser;
using API.DTOs.User.StatisticalUser;
using API.DTOs.User.UpdateUser;
using API.Helpers;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using Common.Constant;
using Common.DataType;
using Common.Enums;
using Common.Helpers;
using Common.Jwt;
using Data.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services.Implements
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;

        public UsersService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Response> ChangePasswordAsync(ChangePasswordRequest request)
        {
            using (var transaction = _userRepository.DatabaseTransaction())
            {
                try
                {
                    var user = await _userRepository.GetAsync(user => user.Id == request.Id);

                    if (user == null)
                    {
                        return new Response(false, ErrorMessages.NotFound);
                    }

                    if (!HashStringHelper.IsValid(request.OldPassword, user.Password))
                    {
                        return new Response(false, ErrorMessages.OldPasswordError);
                    }

                    user.Password = HashStringHelper.HashString(request.NewPassword);

                    _userRepository.Update(user);

                    _userRepository.SaveChanges();

                    transaction.Commit();

                    return new Response(true, Messages.ActionSuccess);

                }
                catch
                {
                    transaction.Rollback();

                    return new Response(false, ErrorMessages.BadRequest);
                }
            }
        }

        public async Task<Response<CreateUserResponse>> CreateUserAsync(CreateUserRequest request)
        {
            using (var transaction = _userRepository.DatabaseTransaction())
            {
                try
                {
                    var user = await _userRepository.GetAsync(user => user.UserName == request.UserName ||
                                                                        user.Email == request.Email ||
                                                                        user.PhoneNumber == request.PhoneNumber);
                    if(user != null)
                    {
                        return new Response<CreateUserResponse>(false, ErrorMessages.UserDuplicate);
                    }

                    var userAge = UserNameHelper.GetAge(request.DoB);

                    if(userAge < Settings.MinimumUserAge)
                    {
                        return new Response<CreateUserResponse>(false, ErrorMessages.InvalidAge);
                    }

                    var newpassword = HashStringHelper.HashString(request.Password);

                    var newEntity = new User
                    {
                        UserName = request.UserName,
                        Password = newpassword,
                        FullName = request.FullName,
                        DoB = request.DoB,
                        Email = request.Email,
                        PhoneNumber = request.PhoneNumber,
                        Role = request.Role,
                        Department = request.Department,
                    };

                    var newUser = _userRepository.Create(newEntity);

                    var responseData = new CreateUserResponse(newUser);

                    _userRepository.SaveChanges();

                    transaction.Commit();

                    return new Response<CreateUserResponse>(true, Messages.ActionSuccess, responseData);
                }
                catch
                {
                    transaction.Rollback();

                    return new Response<CreateUserResponse>(false, ErrorMessages.BadRequest);
                }
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            using (var transaction = _userRepository.DatabaseTransaction())
            {
                try
                {
                    var user = await _userRepository.GetAsync(user => user.Id == id);

                    if (user == null)
                    {
                        return false;
                    }

                    _userRepository.Delete(user);

                    _userRepository.SaveChanges();

                    transaction.Commit();

                    return true;
                }
                catch
                {
                    transaction.Rollback();

                    return false;
                }
            }
        }

        public async Task<IEnumerable<GetUserResponse>> GetAllAsync()
        {
            return (await _userRepository.GetAllAsync())
                .Select(user => new GetUserResponse(user));
        }

        public async Task<Response<GetUserResponse>> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetAsync(user => user.Id == id);

            if (user == null)
            {
                return new Response<GetUserResponse>(false, ErrorMessages.NotFound);
            }

            var reponseDate = new GetUserResponse(user);

            return new Response<GetUserResponse>(true, Messages.ActionSuccess, reponseDate);
        }

        public async Task<Response<GetListUsersResponse>> GetPagedListAsync(GetListUsersRequest request)
        {
            var users = (await _userRepository.GetAllAsync()).Select(user => new GetUserResponse(user))
                                        .AsQueryable();

            var validSearchFields = new[]
            {
                ModelField.UserName,
                ModelField.FullName,
                ModelField.Email,
            };

            var validSortFields = new[]
            {
                ModelField.UserName,
                ModelField.FullName
            };

            var validFilterFields = new[]
            {
                ModelField.Role
            };

            var processedList = users.SearchByField(validSearchFields, request.SearchQuery.SearchValue)
                                        .SortByField(validSortFields,request.SortQuery.SortField,
                                                request.SortQuery.SortDirection)
                                        .FilterByField(validFilterFields, request.FilterQuery.FilterField
                                            ,request.FilterQuery.FilterValue);

            var paginatedList = new PagedList<GetUserResponse>(processedList, request.PagingQuery.PageIndex
                                                                , request.PagingQuery.PageSize);

            var response = new GetListUsersResponse(paginatedList);

            return new Response<GetListUsersResponse>(true, Messages.ActionSuccess, response);
        }

        public async Task<LoginResponse?> LoginUserAsync(LoginRequest request)
        {
            var user = await _userRepository.GetAsync(user => user.UserName == request.UserName);
            if (user == null || !HashStringHelper.IsValid(request.Password, user.Password))
            {
                return null;
            }
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                new Claim(ClaimTypes.Role,user.Role.ToString()),
                new Claim("Id",user.Id.ToString()),
                new Claim("UserName", user.UserName.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConstant.Key));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expired = DateTime.UtcNow.AddMinutes(JwtConstant.ExpiredTime);

            var token = new JwtSecurityToken(JwtConstant.Issuer,
                JwtConstant.Audience, claims,
                expires: expired, signingCredentials: signIn);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                FullName = user.FullName,
                Role = user.Role.ToString(),
                Token = tokenString
            };
        }

        public async Task<Response<UpdateUserResponse>> UpdateUserAsync(UpdateUserRequest request)
        {
            using (var transaction = _userRepository.DatabaseTransaction())
            {
                try
                {
                    var user = await _userRepository.GetAsync(user => user.Id == request.Id);

                    if (user != null)
                    {
                        user.FullName = request.FullName;
                        user.DoB = request.DoB;
                        user.PhoneNumber = request.PhoneNumber;
                        user.Department = request.Department;

                        var updateUser = _userRepository.Update(user);

                        var responseData = new UpdateUserResponse(updateUser);

                        _userRepository.SaveChanges();

                        transaction.Commit();

                        return new Response<UpdateUserResponse>(true, Messages.ActionSuccess, responseData);
                    }

                    return new Response<UpdateUserResponse>(false, ErrorMessages.NotFound);
                }
                catch
                { 
                    transaction.Rollback();

                    return new Response<UpdateUserResponse>(false, ErrorMessages.BadRequest);
                }
            }
        }
        public async Task<Response<StatisticalUserResponse>> countEmployee()
        {
            var data = await _userRepository.GetAllAsync();

            return new Response<StatisticalUserResponse>(true, Messages.ActionSuccess, new StatisticalUserResponse(data.Count()));
        }

        public async Task<Response<StatisticalDepartmentResponse>> countDepartment()
        {
            List<User> users = (List<User>)await _userRepository.GetAllAsync();

            List<StatisticalDepartmentItem> detailDepartments = new List<StatisticalDepartmentItem>();

            int totalDepartment = 0;
            foreach (User user in users)
            {
                bool isAdd = false;

                foreach (StatisticalDepartmentItem department in detailDepartments)
                {
                    if (department.departmentName.Equals(user.Department.ToString()))
                    {
                        department.total++;
                        isAdd = true;
                    }
                }

                if (!isAdd)
                {
                    StatisticalDepartmentItem departmentItem = new StatisticalDepartmentItem(user.Department.ToString(), 1);
                    detailDepartments.Add(departmentItem);
                }

                totalDepartment++;
            }

            StatisticalDepartmentResponse dataResponse = new StatisticalDepartmentResponse();
            dataResponse.totalDepartments = totalDepartment;
            dataResponse.details = detailDepartments;

            return new Response<StatisticalDepartmentResponse>(true, Messages.ActionSuccess, dataResponse);
        }

    }

}
