using API.DTOs.Category.CreateCategory;
using API.DTOs.Category.GetCategory;
using API.DTOs.Category.GetListCategories;
using API.DTOs.Category.StatisticalCategory;
using Common.DataType;

namespace API.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<Response<CreateCategoryResponse>> CreateCategoryAsync(CreateCategoryRequest request);

        Task<bool> DeleteCategoryAsync(int id);

        Task<Response<GetCategoryResponse>> GetByIdAsync(int id);

        Task<IEnumerable<GetCategoryResponse>> GetAllAsync();

        Task<Response<StatisticalCateResponse>> countCatalog();

        Task<Response<GetListCategoriesResponse>> GetPagedListAsync(GetListCategoriesRequest request);
    }
}
