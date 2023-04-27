using API.DTOs.Event.GetEvent;
using Common.DataType;

namespace API.DTOs.Event.GetListEvents
{
    public class GetListEventsResponse
    {
        public GetListEventsResponse(PagedList<GetEventResponse> pagedList)
        {
            Result = pagedList;
        }

        public PagedList<GetEventResponse> Result { get; set; }
    }
}
