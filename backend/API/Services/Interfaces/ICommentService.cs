using API.DTOs.Comment.CreateComment;
using API.DTOs.Comment.GetComment;
using Common.DataType;

namespace API.Services.Interfaces
{
    public interface ICommentService
    {
        Task<Response<CreateCommentResponse>> CreateCommentAsync(CreateCommentRequest request);

        Task<Response<GetCommentResponse>> GetByIdAsync(int id);

        Task<IEnumerable<GetCommentResponse>> GetAllAsync();
    }
}
