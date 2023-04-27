using API.Queries.Ideas;
using API.Queries;
using API.Queries.Events;

namespace API.DTOs.Event.GetListEvents
{
    public class GetListEventsRequest
    {
        public GetListEventsRequest(
        PagingQuery pagingQuery,
        SortQuery sortQuery,
        SearchQuery searchQuery,
        EventFilter eventFilter)
        {
            PagingQuery = pagingQuery;
            SortQuery = sortQuery;
            SearchQuery = searchQuery;
            EventFilter = eventFilter;
        }

        public PagingQuery PagingQuery { get; }

        public SortQuery SortQuery { get; }

        public SearchQuery SearchQuery { get; }

        public EventFilter EventFilter { get; }
    }
}
