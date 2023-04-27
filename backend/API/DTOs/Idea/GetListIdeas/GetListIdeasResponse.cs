using API.DTOs.Idea.GetIdea;
using Common.DataType;

namespace API.DTOs.Idea.GetListIdeas
{
    public class GetListIdeasResponse
    {
        public GetListIdeasResponse(PagedList<GetIdeaResponse> pagedList)
        {
            Result = pagedList;
        }

        public PagedList<GetIdeaResponse> Result { get; set; }
    }
}
