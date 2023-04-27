using Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.User.CreateUser
{
    public class CreateUserResponse
    {
        public CreateUserResponse(Data.Entities.User user)
        {
            UserName = user.UserName;
            FullName = user.FullName;
            DoB = user.DoB.ToString("dd/MM/yyyy");
            Email = user.Email;
            PhoneNumber= user.PhoneNumber;
            Role = user.Role;
            Department = user.Department;
        }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string DoB { get; set; }

        public string Email { get; set; }

        public int PhoneNumber { get; set; }

        public UserRoleEnum Role { get; set; }

        public DepartmentEnum Department { get; set; }
    }
}
