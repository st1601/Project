using API.Queries;

namespace API.DTOs.Category.GetListCategories
{
    public class GetListCategoriesRequest
    {
        public GetListCategoriesRequest(
        PagingQuery pagingQuery,
        SortQuery sortQuery,
        SearchQuery searchQuery)
        {
            PagingQuery = pagingQuery;
            SortQuery = sortQuery;
            SearchQuery = searchQuery;
        }

        public PagingQuery PagingQuery { get; }

        public SortQuery SortQuery { get; }

        public SearchQuery SearchQuery { get; }
    }
}
