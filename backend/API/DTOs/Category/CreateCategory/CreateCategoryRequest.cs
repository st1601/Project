using System.ComponentModel.DataAnnotations;
using System.Security;

namespace API.DTOs.Category.CreateCategory
{
    public class CreateCategoryRequest
    {
        [Required]
        public string? CategoryName { get; set; }

        public string? CategoryDescription { get; set; }
    }
}
