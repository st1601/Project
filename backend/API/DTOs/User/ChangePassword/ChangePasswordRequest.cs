using System.ComponentModel.DataAnnotations;

namespace API.DTOs.User.ChangePassword
{
    public class ChangePasswordRequest
    {
        public int Id { get; set; }

        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}
