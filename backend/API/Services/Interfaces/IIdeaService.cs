using API.DTOs.Idea.CreateIdea;
using API.DTOs.Idea.GetIdea;
using API.DTOs.Idea.GetListIdeas;
using API.DTOs.Idea.Statistical;
using API.DTOs.Idea.UpdateIdea;
using Common.DataType;

namespace API.Services.Interfaces
{
    public interface IIdeaService
    {
        Task<Response<CreateIdeaResponse>> CreateIdeaAsync(CreateIdeaRequest request);

        Task<Response<GetIdeaResponse>> GetByIdAsync(int id);

        Task<IEnumerable<GetIdeaResponse>> GetAllAsync();

        Task<Response<GetListIdeasResponse>> GetPagedListAsync(GetListIdeasRequest request);

        Task<Response<UpdateIdeaResponse>> UpdateIdeaAsync(UpdateIdeaRequest request);

        Task<bool> DeleteIdeaAsync(int id);

        Task<Response<StatisticalIdeaResponse>> countIdeas();

        Task<Response<List<GetTopIdeaResponse>>> getTopFiveIdeasByComment();
    }
}
