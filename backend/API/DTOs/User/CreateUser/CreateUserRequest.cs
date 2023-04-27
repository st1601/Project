using Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.User.CreateUser
{
    public class CreateUserRequest
    {
        [Required, MaxLength(50)]
        public string? UserName { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required, MaxLength(225)]
        public string? FullName { get; set; }

        [Required]
        public DateTime DoB { get; set; }

        [Required, MaxLength(225)]
        public string? Email { get; set; }

        [Required]
        public int PhoneNumber { get; set; }

        [Required]
        public UserRoleEnum Role { get; set; }

        public DepartmentEnum Department { get; set; }
    }
}
