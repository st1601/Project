using API.DTOs.User.GetUser;
using Common.DataType;

namespace API.DTOs.User.GetListUsers
{
    public class GetListUsersResponse
    {
        public GetListUsersResponse(PagedList<GetUserResponse> pagedList)
        {
            Result = pagedList;
        }

        public PagedList<GetUserResponse> Result { get; set; }
    }
}
