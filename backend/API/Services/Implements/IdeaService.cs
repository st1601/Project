using API.DTOs.Idea.Count;
using API.DTOs.Idea.CreateIdea;
using API.DTOs.Idea.GetIdea;
using API.DTOs.Idea.GetListIdeas;
using API.DTOs.Idea.Statistical;
using API.DTOs.Idea.UpdateIdea;
using API.Helpers;
using API.Helpers.EmailHelper;
using API.Queries;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using Common.Constant;
using Common.DataType;
using Common.Enums;
using Data.Entities;

namespace API.Services.Implements
{
    public class IdeaService : IIdeaService
    {
        private readonly IIdeaRepository _ideaRepository;

        private readonly IUserRepository _userRepository;

        private readonly IEventRepository _eventRepository;

        private readonly ICategoryRepository _categoryRepository;

        private readonly ICommentRepository _commentRepository;

        private readonly IEmailService _emailService;

        public IdeaService(IIdeaRepository ideaRepository, IUserRepository userRepository
            , IEventRepository eventRepository, ICategoryRepository categoryRepository
            , IEmailService emailService, ICommentRepository commentRepository)
        {
            _ideaRepository = ideaRepository;
            _userRepository = userRepository;
            _eventRepository = eventRepository;
            _categoryRepository = categoryRepository;
            _emailService = emailService;
            _commentRepository = commentRepository;
        }

        public async Task<Response<CreateIdeaResponse>> CreateIdeaAsync(CreateIdeaRequest request)
        {
            using (var trasaction = _ideaRepository.DatabaseTransaction())
            {
                try
                {
                    var user = await _userRepository.GetAsync(user => user.Id == request.UserId);

                    var events = await _eventRepository.GetAsync(events => events.Id == request.EventId);

                    if (user == null || events == null) 
                    { 
                        return new Response<CreateIdeaResponse>(false, ErrorMessages.NotFound); 
                    }

                    var categoryIds = request.CategoryIds.Distinct();

                    var categories = (List<Category>) await _categoryRepository
                        .GetAllAsync(category => categoryIds.Contains(category.Id));

                    if (categories.Count != categoryIds.Count())
                    {
                        return new Response<CreateIdeaResponse>(false, ErrorMessages.NotFound);
                    }

                    if (user.Department != events.User.Department)
                    {
                        return new Response<CreateIdeaResponse>(false, ErrorMessages.InvalidDepartment);
                    }

                    var newEntity = new Idea
                    {
                        IdeaTitle = request.IdeaTitle,
                        IdeaDescription = request.IdeaDescription,
                        DateSubmitted = DateTime.UtcNow,
                        File = request.File,
                        HashTag= request.HashTag,
                        UserId = request.UserId,
                        EventId = request.EventId,
                        Categories = categories
                    };

                    if( newEntity.DateSubmitted < events.FirstClosingDate || newEntity.DateSubmitted > events.LastClosingDate)
                    {
                        return new Response<CreateIdeaResponse>(false, ErrorMessages.InvalidDateSubmitted);
                    }

                    var newIdea = _ideaRepository.Create(newEntity);

                    var message = new Message(new string[] { events.User.Email }, "Got a new idea!!!"
                        , "Full Name: " + user.FullName + "\nDate Submitted: " 
                        + newIdea.DateSubmitted.ToString("dd/MM/yyyy") + "\nTitle Idea: " + newIdea.IdeaTitle 
                        + "\nIdea Description: " + newIdea.IdeaDescription + "\n\n\n" + newIdea.File);

                    await _emailService.SendEmailAsync(message);

                    var responseData = new CreateIdeaResponse(newIdea);

                    _ideaRepository.SaveChanges();

                    trasaction.Commit();

                    return new Response<CreateIdeaResponse>(true, Messages.ActionSuccess , responseData);
                }
                catch
                {
                    trasaction.Rollback();

                    return new Response<CreateIdeaResponse>(false, ErrorMessages.BadRequest);
                }
            }
        }

        public async Task<bool> DeleteIdeaAsync(int id)
        {
            using (var transaction = _ideaRepository.DatabaseTransaction())
            {
                try
                {
                    var idea = await _ideaRepository.GetAsync(idea => idea.Id == id);

                    if ( idea == null) { return false; }

                    _ideaRepository.Delete(idea);

                    _ideaRepository.SaveChanges();

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

        public async Task<IEnumerable<GetIdeaResponse>> GetAllAsync()
        {
            return (await _ideaRepository.GetAllAsync())
                .Select(idea => new GetIdeaResponse(idea));
        }

        public async Task<Response<GetIdeaResponse>> GetByIdAsync(int id)
        {
            var idea = await _ideaRepository.GetAsync(idea => idea.Id == id);

            if(idea == null)
            {
                return new Response<GetIdeaResponse>(false, ErrorMessages.NotFound);
            }

            var responseData = new GetIdeaResponse(idea);

            return new Response<GetIdeaResponse>(true, Messages.ActionSuccess, responseData);
        }

        public async Task<Response<GetListIdeasResponse>> GetPagedListAsync(GetListIdeasRequest request)
        {
            var ideas = (await _ideaRepository.GetAllAsync()).Select(i => new GetIdeaResponse(i)).AsQueryable();

            var validSearchFields = new[]
            {
                ModelField.HashTag,
                ModelField.UserName
            };

            var validSortFields = new[]
            {
                ModelField.IdeaTitle,
                ModelField.DateSubmitted
            };

            var validFilterFields = new[]
            {
                ModelField.Department,
                ModelField.CategoryName,
                ModelField.EventName
            };

            var filterQueries = new List<FilterQuery>();

            if (!string.IsNullOrEmpty(request.IdeaFilter.Department))
            {
                filterQueries.Add(new FilterQuery
                {
                    FilterField = ModelField.Department,
                    FilterValue = request.IdeaFilter.Department
                });
            }

            if (!string.IsNullOrEmpty(request.IdeaFilter.CategoryName))
            {
                filterQueries.Add(new FilterQuery
                {
                    FilterField = ModelField.CategoryName,
                    FilterValue = request.IdeaFilter.CategoryName
                });
            }

            if (!string.IsNullOrEmpty(request.IdeaFilter.EventName))
            {
                filterQueries.Add(new FilterQuery
                {
                    FilterField = ModelField.EventName,
                    FilterValue = request.IdeaFilter.EventName
                });
            }

            var processedList = ideas.MultipleFiltersByField(validFilterFields, filterQueries)
                .SortByField(validSortFields, request.SortQuery.SortField, request.SortQuery.SortDirection)
                .SearchByField(validSearchFields, request.SearchQuery.SearchValue);

            var pagedList = new PagedList<GetIdeaResponse>(processedList, request.PagingQuery.PageIndex
                                                            ,request.PagingQuery.PageSize);

            var responseData = new GetListIdeasResponse(pagedList);

            return new Response<GetListIdeasResponse>(true, Messages.ActionSuccess, responseData);
        }

        public async Task<Response<UpdateIdeaResponse>> UpdateIdeaAsync(UpdateIdeaRequest request)
        {
            using ( var trasaction = _ideaRepository.DatabaseTransaction())
            {
                try
                {
                    var idea = await _ideaRepository.GetAsync(idea => idea.Id == request.Id);

                    var categoryIds = request.CategoryIds.Distinct();

                    var categories = (List<Category>)await _categoryRepository
                        .GetAllAsync(category => categoryIds.Contains(category.Id));

                    if (categories.Count != categoryIds.Count())
                    {
                        return new Response<UpdateIdeaResponse>(false, ErrorMessages.NotFound);
                    }

                    if (idea != null)
                    {
                        var user = await _userRepository.GetAsync(user => user.Id == request.UserId);

                        var events = await _eventRepository.GetAsync(events => events.Id == request.EventId);

                        idea.IdeaTitle = request.IdeaTitle;
                        idea.IdeaDescription = request.IdeaDescription;
                        idea.DateSubmitted = DateTime.UtcNow;
                        idea.File = request.File;
                        idea.HashTag = request.HashTag;
                        idea.UserId = request.UserId;
                        idea.EventId = request.EventId;
                        idea.Categories = categories;

                        if (idea.DateSubmitted < events.FirstClosingDate || idea.DateSubmitted > events.LastClosingDate)
                        {
                            return new Response<UpdateIdeaResponse>(false, ErrorMessages.InvalidDateSubmitted);
                        }

                        var updateIdea = _ideaRepository.Update(idea);

                        var responseData = new UpdateIdeaResponse(updateIdea);

                        _ideaRepository.SaveChanges();

                        trasaction.Commit();

                        return new Response<UpdateIdeaResponse>(true, Messages.ActionSuccess, responseData);
                    }

                    return new Response<UpdateIdeaResponse>(false, ErrorMessages.NotFound);
                }
                catch
                {
                    trasaction.Rollback();

                    return new Response<UpdateIdeaResponse>(false, ErrorMessages.BadRequest);
                }
            }
        }

        public async Task<Response<StatisticalIdeaResponse>> countIdeas()
        {
            StatisticalIdeaResponse dataResponse = new StatisticalIdeaResponse();

            List<Event> events = (List<Event>)await _eventRepository.GetAllAsync();

            if (events.Count > 0)
            {
                int totalIdeas = 0;

                foreach (Event e in events)
                {
                    int countIdeas = (await _ideaRepository.GetAllAsync(idea => idea.EventId == e.Id)).Count();

                    StatisticalIdeaItem items = new StatisticalIdeaItem(e.Id, e.EventName, countIdeas);
                    dataResponse.details.Add(items);

                    totalIdeas += countIdeas;
                }

                dataResponse.totalIdeas = totalIdeas;
            }

            return new Response<StatisticalIdeaResponse>(true, Messages.ActionSuccess, dataResponse);
        }

        public async Task<Response<List<GetTopIdeaResponse>>> getTopFiveIdeasByComment()
        {
            List<Idea> ideas = (List<Idea>)await _ideaRepository.GetAllAsync();

            List<IdeaCount> ideaCounts = new List<IdeaCount>();
            int[] topCount = new int[5] { 0, 0, 0, 0, 0 };

            foreach (Idea idea in ideas)
            {
                int countByIdea = (await _commentRepository.GetAllAsync(comment => comment.IdeaId == idea.Id)).Count();

                ideaCounts.Add(new IdeaCount(idea, countByIdea));
            }

            ideaCounts.Sort(IdeaCount.compareIdeaCount);

            int count = 0;
            List<GetTopIdeaResponse> dataResponse = new List<GetTopIdeaResponse>();
            foreach (IdeaCount idea in ideaCounts)
            {
                if (count >= 5) break;
                dataResponse.Add(new GetTopIdeaResponse(new GetIdeaResponse(idea.idea), idea.count));
                count++;
            }

            return new Response<List<GetTopIdeaResponse>>(true, Messages.ActionSuccess, dataResponse);

        }
    }
}
