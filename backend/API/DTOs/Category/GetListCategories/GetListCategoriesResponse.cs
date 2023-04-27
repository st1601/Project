using API.DTOs.Category.GetCategory;
using Common.DataType;

namespace API.DTOs.Category.GetListCategories
{
    public class GetListCategoriesResponse
    {
        public GetListCategoriesResponse(PagedList<GetCategoryResponse> pagedList)
        {
            Result = pagedList;
        }

        public PagedList<GetCategoryResponse> Result { get; set; }
    }
}
