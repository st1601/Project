using Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class User : BaseEntity
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
        public DepartmentEnum Department { get; set; }

        [Required]
        public UserRoleEnum Role { get; set; }

        public ICollection<Comment>? Comments { get; set; }

        public ICollection<Idea>? Ideas { get; set; }

        public ICollection<Thumb>? Thumbs { get; set; }

        public ICollection<Event>? Events { get; set; }
    }
}
