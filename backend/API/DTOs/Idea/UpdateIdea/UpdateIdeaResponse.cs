using API.DTOs.Category;
using API.DTOs.Event.GetEvent;
using API.DTOs.User.GetUser;
using Common.Enums;

namespace API.DTOs.Idea.UpdateIdea
{
    public class UpdateIdeaResponse
    {
        public UpdateIdeaResponse(Data.Entities.Idea idea)
        {
            Id = idea.Id;
            IdeaTitle = idea.IdeaTitle;
            IdeaDescription = idea.IdeaDescription;
            DateSubmitted = idea.DateSubmitted;
            File = idea.File;
            UserName = idea.User.UserName;
            Department = idea.User.Department;
            EventName = idea.Event.EventName;
            FirstClosingDate = idea.Event.FirstClosingDate.ToString("dd/MM/yyyy");
            LastClosingDate = idea.Event.LastClosingDate.ToString("dd/MM/yyyy");
            Categories = idea.Categories
                .Select(category => new CategoryModel
                {
                    Id = category.Id,
                    CategoryName = category.CategoryName,
                    CategoryDescription = category.CategoryDescription,
                }).ToList();
        }

        public int Id { get; set; }
        public string IdeaTitle { get; set; }

        public string IdeaDescription { get; set; }

        public string File { get; set; }

        public string HashTag { get; set; }

        public DateTime DateSubmitted { get; set; }

        public string UserName { get; set; }

        public DepartmentEnum Department { get; set; }

        public string EventName { get; set; }

        public string FirstClosingDate { get; set; }

        public string LastClosingDate { get; set; }

        public List<CategoryModel> Categories { get; set; }
    }
}
