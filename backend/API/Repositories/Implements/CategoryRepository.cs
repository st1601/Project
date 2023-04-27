using API.Repositories.Interfaces;
using Data;
using Data.Entities;

namespace API.Repositories.Implements
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
