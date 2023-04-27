using API.Queries;

namespace API.DTOs.User.GetListUsers
{
    public class GetListUsersRequest
    {
        public GetListUsersRequest(
        PagingQuery pagingQuery,
        SortQuery sortQuery,
        FilterQuery filterQuery,
        SearchQuery searchQuery)
        {
            PagingQuery = pagingQuery;
            SortQuery = sortQuery;
            FilterQuery = filterQuery;
            SearchQuery = searchQuery;
        }

        public PagingQuery PagingQuery { get; }

        public SortQuery SortQuery { get; }

        public FilterQuery FilterQuery { get; }

        public SearchQuery SearchQuery { get; }
    }
}
