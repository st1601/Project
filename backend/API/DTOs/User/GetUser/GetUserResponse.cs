using Common.Enums;

namespace API.DTOs.User.GetUser
{
    public class GetUserResponse
    {
        public GetUserResponse(Data.Entities.User user)
        {
            Id = user.Id;
            UserName = user.UserName;
            FullName = user.FullName;
            DoB = user.DoB.ToString("dd/MM/yyyy");
            Email = user.Email;
            PhoneNumber = user.PhoneNumber;
            Role = user.Role.ToString();
            Department = user.Department.ToString();
        }

        public int Id { get; set; }

        public string UserName { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string DoB { get; set; }

        public string Email { get; set; } = null!;

        public int PhoneNumber { get; set; } 

        public string Role { get; set; }

        public string Department { get; set; }
    }
}
