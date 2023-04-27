using API.DTOs.Thumbs.GetThumbs;
using API.DTOs.Thumbs.Thumb;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using Common.Constant;
using Common.DataType;
using Data.Entities;

namespace API.Services.Implements
{
    public class ThumbsService : IThumbsService
    {
        private readonly IThumbRespository _thumbRespository;

        private readonly IIdeaRepository _ideaRepository;

        public ThumbsService (IThumbRespository thumbRespository, IIdeaRepository ideaRepository)
        {
            _thumbRespository = thumbRespository;
            _ideaRepository = ideaRepository;
        }

        public async Task<Response<GetThumbResponse>> CountThumbsByIdeaIdAsync(int id)
        {
            var idea = (await _ideaRepository.GetAsync(idea => idea.Id == id)).IdeaTitle;

            if (idea == null)
            {
                return new Response<GetThumbResponse>(false, ErrorMessages.NotFound);
            }

            int countThumbUp = (await _thumbRespository.GetAllAsync(t => t.IdeaId == id 
                                        && t.ThumbType == Common.Enums.ThumbEnum.ThumbUp)).Count();


            int countThumbDown = (await _thumbRespository.GetAllAsync(t => t.IdeaId == id
                                        && t.ThumbType == Common.Enums.ThumbEnum.ThumbDown)).Count();

            var dataResponse = new GetThumbResponse(idea, countThumbUp, countThumbDown);

            return new Response<GetThumbResponse>(true, Messages.ActionSuccess, dataResponse);
        }

        public async Task<Response<ThumbResponse>> CreateThumbAsync(ThumbRequest request)
        {
            using (var transaction = _thumbRespository.DatabaseTransaction())
            {
                try
                {
                    var idea = await _ideaRepository.GetAsync(idea => idea.Id == request.IdeaId);

                    if (idea == null)
                    {
                        return new Response<ThumbResponse>(false, ErrorMessages.NotFound);
                    }

                    var thumb = await _thumbRespository.GetAsync(thumb => thumb.User.Id == request.UserId);

                    if (thumb != null)
                    {
                        return new Response<ThumbResponse>(false, ErrorMessages.InvalidThumb);
                    }

                    var newEntity = new Thumb
                    {
                        ThumbType = request.ThumbType,
                        IdeaId = request.IdeaId,
                        UserId = request.UserId
                    };

                    var newThumb = _thumbRespository.Create(newEntity);

                    _thumbRespository.SaveChanges();

                    transaction.Commit();

                    var responseData = new ThumbResponse(newThumb);

                    return new Response<ThumbResponse>(true, Messages.ActionSuccess, responseData);
                }
                catch
                {
                    transaction.Rollback();

                    return new Response<ThumbResponse>(false, ErrorMessages.BadRequest);
                }
            } 
        }

        public async Task<bool> DeleteThumbAsync(int id)
        {
            using (var transaction = _thumbRespository.DatabaseTransaction())
            {
                try
                {
                    var thumb = await _thumbRespository.GetAsync(thumb => thumb.Id == id);

                    if (thumb == null) { return false; }

                    _thumbRespository.Delete(thumb);

                    _thumbRespository.SaveChanges();

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
    }
}
