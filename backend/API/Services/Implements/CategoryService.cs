using API.DTOs.Category.CreateCategory;
using API.DTOs.Category.GetCategory;
using API.DTOs.Category.GetListCategories;
using API.DTOs.Category.StatisticalCategory;
using API.Helpers;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using Common.Constant;
using Common.DataType;
using Common.Enums;
using Data.Entities;

namespace API.Services.Implements
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Response<CreateCategoryResponse>> CreateCategoryAsync(CreateCategoryRequest request)
        {
            using (var transaction = _categoryRepository.DatabaseTransaction())
            {
                try
                {
                    var newEntity = new Category
                    {
                        CategoryName = request.CategoryName,
                        CategoryDescription = request.CategoryDescription
                    };

                    var newCategory = _categoryRepository.Create(newEntity);

                    var responseData = new CreateCategoryResponse(newCategory);

                    _categoryRepository.SaveChanges();

                    transaction.Commit();

                    return new Response<CreateCategoryResponse>(true, Messages.ActionSuccess, responseData);
                }
                catch
                {
                    transaction.Rollback();

                    return new Response<CreateCategoryResponse>(false, ErrorMessages.BadRequest);
                }
            }
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            using (var transaction = _categoryRepository.DatabaseTransaction())
            {
                try
                {
                    var category = await _categoryRepository.GetAsync(category => category.Id == id);

                    if (category == null)
                    {
                        return false;
                    }

                    _categoryRepository.Delete(category);

                    _categoryRepository.SaveChanges();

                    transaction.Commit();

                    return true;
                }
                catch
                {
                    transaction.Rollback();

                    return false;
                }
            }
        }

        public async Task<IEnumerable<GetCategoryResponse>> GetAllAsync()
        {
            return ( await _categoryRepository.GetAllAsync())
                .Select(category => new GetCategoryResponse(category));
        }

        public async Task<Response<GetCategoryResponse>> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetAsync(category => category.Id == id);

            if(category == null)
            {
                return new Response<GetCategoryResponse>(false, ErrorMessages.NotFound);
            }
            var responseData = new GetCategoryResponse(category);

            return new Response<GetCategoryResponse>(true, Messages.ActionSuccess, responseData);
        }

        public async Task<Response<StatisticalCateResponse>> countCatalog()
        {
            var data = await _categoryRepository.GetAllAsync();

            return new Response<StatisticalCateResponse>(true, Messages.ActionSuccess, new StatisticalCateResponse(data.Count()));
        }

        public async Task<Response<GetListCategoriesResponse>> GetPagedListAsync(GetListCategoriesRequest request)
        {
            var categories = (await _categoryRepository.GetAllAsync())
                                .Select(category => new GetCategoryResponse(category)).AsQueryable();

            var validSearchFields = new[]
            {
                ModelField.CategoryName
            };

            var validSortFields = new[]
            {
                ModelField.CategoryName
            };

            var processedList = categories.SearchByField(validSearchFields, request.SearchQuery.SearchValue)
                                 .SortByField(validSortFields, request.SortQuery.SortField, request.SortQuery.SortDirection);

            var pagedList = new PagedList<GetCategoryResponse>(processedList, request.PagingQuery.PageIndex
                                                                    , request.PagingQuery.PageSize);

            var responseData = new GetListCategoriesResponse(pagedList);

            return new Response<GetListCategoriesResponse>(true, Messages.ActionSuccess, responseData);
        }
    }
}

