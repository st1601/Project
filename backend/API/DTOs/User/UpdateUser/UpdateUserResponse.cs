using Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.User.UpdateUser
{
    public class UpdateUserResponse
    {
        public UpdateUserResponse(Data.Entities.User user)
        {
            Id = user.Id;
            FullName = user.FullName;
            DoB = user.DoB.ToString("dd/MM/yyyy");
            PhoneNumber = user.PhoneNumber;
            Department = user.Department;
        }

        public int Id { get; set; }

        public string? FullName { get; set; }

        public string DoB { get; set; }

        public int PhoneNumber { get; set; }

        public DepartmentEnum Department { get; set; }
    }
}
