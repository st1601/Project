using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Category : BaseEntity
    {
        [Required, MaxLength(225)]
        public string? CategoryName { get; set; }

        [Required]
        public string? CategoryDescription { get; set; }

        public ICollection<Idea>? Ideas { get; set; }
    }
}
