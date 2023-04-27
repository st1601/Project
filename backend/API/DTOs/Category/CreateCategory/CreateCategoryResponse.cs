namespace API.DTOs.Category.CreateCategory
{
    public class CreateCategoryResponse
    {
        public CreateCategoryResponse(Data.Entities.Category category)
        {
            CategoryName= category.CategoryName;
            CategoryDescription= category.CategoryDescription;
        }

        public string? CategoryName { get; set; }

        public string? CategoryDescription { get; set; }
    }
}
